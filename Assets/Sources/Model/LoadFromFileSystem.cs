using Mono.Cecil;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace Wallet
{
    public partial class LoadFromFileSystem : SystemBase
    {
        private EntityQuery _requiredQuery;

        protected override void OnCreate()
        {
            _requiredQuery = GetEntityQuery(ComponentType.ReadOnly(typeof(LoadFromFileComponent)));
            RequireForUpdate(_requiredQuery);
        }

        protected override void OnUpdate()
        {
            var entities = _requiredQuery.ToEntityArray(Allocator.Temp);
            var saveComponent = EntityManager.GetComponentData<LoadFromFileComponent>(entities.First());
            EntityManager.DestroyEntity(entities);
            
            LoadAsync(saveComponent.FileName.Value + ".txt");
        }

        private async void LoadAsync(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return;
            }

            using (FileStream sourceStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                StringBuilder sb = new StringBuilder();

                byte[] buffer = new byte[0x1000];
                int numRead;
                while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string text = Encoding.Unicode.GetString(buffer, 0, numRead);
                    sb.Append(text);
                }

                JArray result = JArray.Parse(sb.ToString());

                var entities = GetEntityQuery(ComponentType.ReadOnly(typeof(ResourceComponent))).ToEntityArray(Allocator.Temp);
                for (int i = 0; i < entities.Length; i++)
                {
                    var resource = EntityManager.GetComponentData<ResourceComponent>(entities[i]);

                    for (int j = 0; j < result.Count; j++)
                    {
                        ResourceType resourceType = JsonUtility.FromJson<ResourceComponent>(result[j].ToString()).Type;
                        if (resourceType == resource.Type)
                        {
                            EntityManager.AddComponentData(entities[i], new FinishedLoadingComponent(result[j].ToString()));
                        }
                    }
                }
            }
        }
    }
}