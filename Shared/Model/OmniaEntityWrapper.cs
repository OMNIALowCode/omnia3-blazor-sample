using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;

namespace omnia_blazor_demo.Shared.Model
{
    public class OmniaEntityWrapper<T>
    {

        private OmniaClient _omniaClient { get; set; }

        private EntityMetadata _entityMetadata { get; set; }

        private string _temporaryId { get; set; }

        private T _presentationEntity;

        private T _synchronizedEntity;

        public T Entity
        {
            get
            {
                return _presentationEntity;
            }
            private set
            {
                _presentationEntity = value;
            }
        }

        private OmniaEntityWrapper(OmniaClient omniaClient, EntityMetadata entityMetadata)
        {
            _omniaClient = omniaClient;
            _entityMetadata = entityMetadata;

            _presentationEntity = Activator.CreateInstance<T>();
            _synchronizedEntity = Activator.CreateInstance<T>();
        }

        private void ApplyInitialJsonPatchDocument(string temporaryId, JsonPatchDocument patchDocument)
        {
            _temporaryId = temporaryId;

            patchDocument.ApplyTo(_synchronizedEntity);
            patchDocument.ApplyTo(_presentationEntity);
        }

        private void ApplyInitialDocument(string temporaryId, T document) { ApplyInitialJsonPatchDocument(temporaryId, PatchHelper.CompareObjects(_synchronizedEntity, document));}


        public async Task Sync()
        {

            var jsonPatchDocument = PatchHelper.CompareObjects(_synchronizedEntity, _presentationEntity);
            var temporary = await _omniaClient.UpdateTemporary(_entityMetadata, _temporaryId, jsonPatchDocument);
            temporary.data.ApplyTo(_synchronizedEntity);

            var diff = PatchHelper.CompareObjects(_presentationEntity, _synchronizedEntity);
            diff.ApplyTo(_presentationEntity);

        }



        public async Task<string> Save()
        {
            var response = await _omniaClient.SaveTemporary(_entityMetadata, _temporaryId);
            return response.identifier;
        }

        public static async Task<OmniaEntityWrapper<T>> Create(OmniaClient omniaClient, EntityMetadata entityMetadata, string code = null)
        {

            var entityWrapper = new OmniaEntityWrapper<T>(omniaClient, entityMetadata);
            if (string.IsNullOrEmpty(code))
            {
                var temporary = await omniaClient.CreateTemporary(entityMetadata);
                entityWrapper.ApplyInitialJsonPatchDocument(temporary.id, temporary.data);
            }
            else
            {
                var temporary = await omniaClient.CreateTemporary<T>(entityMetadata, code);
                entityWrapper.ApplyInitialDocument(temporary.id, temporary.data);
            }

            return entityWrapper;

        }

    }

}