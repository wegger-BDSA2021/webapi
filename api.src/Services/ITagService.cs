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
    Task<Result> CreateAsync(TagCreateDTO tag);
    Task<Result> ReadAsync(int id);
    Task<Result> UpdateAsync(TagUpdateDTO tag);
    Task<Result> Delete(int id);
    Task<Result> getAllTags();
}