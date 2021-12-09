using System.Threading.Tasks;
using Data;
using Services;
using System;
using static Data.Response;
using Microsoft.AspNetCore.Mvc;
using ResourceBuilder;
using System.Linq;
using System.Collections.Generic;
public interface ITagService
{
    public Task<Result> CreateAsync(TagCreateDTO tag);
    public Task<Result> ReadAsync(int id);
    public Task<Result> UpdateAsync(TagUpdateDTO tag);
    public Task<Result> Delete(int id);
    public Task<IReadOnlyCollection<TagDetailsDTO>> getAllTags();
}