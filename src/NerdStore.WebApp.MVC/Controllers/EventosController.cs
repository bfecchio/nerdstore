using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using NerdStore.Core.Data.EventSourcing;

namespace NerdStore.WebApp.MVC.Controllers
{
    public class EventosController : Controller
    {
        #region Private Read-Only Fields

        private readonly IEventSourcingRepository _eventSourcingRepository;

        #endregion

        #region Constructors

        public EventosController(IEventSourcingRepository eventSourcingRepository)
        {
            _eventSourcingRepository = eventSourcingRepository;
        }

        #endregion

        #region Controller Actions

        [HttpGet("eventos/{id:guid}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var eventos = await _eventSourcingRepository.ObterEventos(id);
            return View(eventos);
        }

        #endregion
    }
}
