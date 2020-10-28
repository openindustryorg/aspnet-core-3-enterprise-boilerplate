using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Core;
using Web.EntityFramework;

namespace Web.Application.Controllers
{

  [ApiExplorerSettings(GroupName = "API")]
  public class GenericBaseController<T> : ControllerBase where T : BaseEntity
  {
    private IRepository<T> repository;
    public Account Account => (Account)HttpContext.Items["Account"];

    public GenericBaseController(IRepository<T> Repository)
    {
      this.repository = Repository;
    }

    [HttpGet("GetAll")]
    public IEnumerable<T> GetAll() => repository.GetAll();

    [HttpGet]
    public T GetById(Guid entityId) => repository.GetById(entityId);

    [HttpPost]
    public void AddEntity([FromBody] T entity) => repository.Insert(entity);

    [HttpDelete]
    public void DeleteEntity(Guid entityId) => repository.Delete(entityId);
  }
}