using RealEstates.Services.Dtos.Import;
using System.Collections.Generic;

namespace RealEstates.Services
{
    public interface ITagService
    {
        void AddTags(IEnumerable<TagInputModel> tagInputModels);

        void AddTagsToPropertiesRelations();
    }
}
