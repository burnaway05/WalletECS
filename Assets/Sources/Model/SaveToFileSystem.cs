using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Unity.Collections;
using Unity.Entities;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEngine;

namespace Wallet
{
    public partial class SaveToFileSystem : SystemBase
    {
        private EntityQuery _requiredQuery;

        protected override void OnCreate()
        {
            _requiredQuery = GetEntityQuery(ComponentType.ReadOnly(typeof(SaveToFileComponent)));
            RequireForUpdate(_requiredQuery);
        }

        protected override void OnUpdate()
        {
            JArray saveInformation = new JArray();
            var entities = GetEntityQuery(ComponentType.ReadOnly(typeof(ResourceComponent))).ToEntityArray(Allocator.Temp);
            for (int i = 0; i < entities.Length; i++)
            {
                var resource = EntityManager.GetComponentData<ResourceComponent>(entities[i]);
                saveInformation.Add(JsonUtility.ToJson(resource));
            }

            entities = _requiredQuery.ToEntityArray(Allocator.Temp);
            var saveComponent = EntityManager.GetComponentData<SaveToFileComponent>(entities.First());
            EntityManager.DestroyEntity(entities);

            WriteAsync(saveComponent.FileName.Value, saveInformation.ToString());
        }

        private async void WriteAsync(string fileName, string text)
        {
            byte[] encodedText = Encoding.Unicode.GetBytes(text);
            string fullName = fileName + ".txt";
            using (FileStream sourceStream = new FileStream(fullName, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            };
        }
    }
}