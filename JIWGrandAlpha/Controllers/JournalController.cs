using JIWGrandAlpha.Domain.ViewModels.Cell;
using JIWGrandAlpha.Domain.ViewModels.Class;
using JIWGrandAlpha.Domain.ViewModels.Journal;
using JIWGrandAlpha.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JIWGrandAlpha.Controllers
{
    [Authorize]
    public class JournalController : Controller
    {
        private readonly IJournalService _journalService;

        public JournalController(IJournalService journalService)
        {
            _journalService = journalService;

        }

        [Authorize(Roles = "Студент")]
        public async Task<IActionResult> Student()
        {
            JStudentViewModel journal = new JStudentViewModel();
            var dates = await _journalService.getDatesS(User.Identity.Name);
            var objects = await _journalService.getObjectsS(User.Identity.Name);
            if (dates.StatusCode == Domain.Enum.StatusCode.OK && objects.StatusCode == Domain.Enum.StatusCode.OK)
            {
                journal.Dates = dates.Data;
                journal.Objects = objects.Data;
                return View(journal);
            }
            ModelState.AddModelError("", $"");
            return View(journal);
        }
        [Authorize(Roles = "Преподаватель")]
        [HttpGet]
        public async Task<IActionResult> ChooseGroup()
        {
            JTeacherViewModel journal = new JTeacherViewModel();
            var groups = await _journalService.getGroupsT(User.Identity.Name);
            if (groups.StatusCode == Domain.Enum.StatusCode.OK)
            {
                journal.Groups = groups.Data;
                return View(journal);
            }
            ModelState.AddModelError("", $"{groups.Description}");
            return View(journal);
        }

        [Authorize(Roles = "Преподаватель")]
        [HttpGet]
        public async Task<IActionResult> ChooseObject(int idGroup)
        {
            JTeacherViewModel journal = new JTeacherViewModel();
            var objects = await _journalService.getObjectsT(User.Identity.Name, idGroup);
            if (objects.StatusCode == Domain.Enum.StatusCode.OK)
            {
                journal.Objects = objects.Data;
                journal.idGroup = idGroup;
                return View(journal);
            }
            ModelState.AddModelError("", $"{objects.Description}");
            return View(journal);
        }

        [Authorize(Roles = "Преподаватель")]
        [HttpGet]
        public async Task<IActionResult> Teacher(int idObject, int idGroup)
        {
            JTeacherViewModel journal = new JTeacherViewModel();
            journal.idObject = idObject;
            journal.idGroup = idGroup;
            var Classes = await _journalService.getClassT(idObject, idGroup);
            var Students = await _journalService.getStudentsT(idObject, idGroup);
            if (Classes.StatusCode == Domain.Enum.StatusCode.OK && Students.StatusCode == Domain.Enum.StatusCode.OK)
            {
                journal.Classes = Classes.Data;
                journal.Students = Students.Data;
                return View(journal);
            }
            ModelState.AddModelError("", $"{Students.Description} | {Classes.Description}");
            return View(journal);
        }

        [HttpGet]
        public async Task<IActionResult> EditClass(long id, long idObject, long idGroup)
        {
            var response = await _journalService.getClass(id, idObject, idGroup);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView(response.Data);
            }
            ModelState.AddModelError("", response.Description);
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> EditClass(ClassViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _journalService.CreateClass(model);
                }
                else
                {
                    await _journalService.EditClass(model.Id, model);
                }
                return RedirectToAction("Teacher");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RemoveClass(long id)
        {
            await _journalService.RemoveClass(id);
            return RedirectToAction("Teacher");
        }
        [HttpGet]
        public async Task<IActionResult> EditCell(long id, long idStudent, long idClass)
        {
            var response = await _journalService.getCell(id, idStudent, idClass);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView(response.Data);
            }
            ModelState.AddModelError("", response.Description);
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> EditCell(CellViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _journalService.CreateCell(model);
                }
                else
                {
                    await _journalService.EditCell(model.Id, model);
                }

                return RedirectToAction("Teacher");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCell(long id)
        {
            await _journalService.RemoveCell(id);
            return RedirectToAction("Teacher");
        }

        [HttpPost]
        public JsonResult GetCellTypes()
        {
            var respsonse = _journalService.getCellsTypes();
            return Json(respsonse.Data);
        }

        [HttpPost]
        public JsonResult GetClassTypes()
        {
            var respsonse = _journalService.getClassTypes();
            return Json(respsonse.Data);
        }
    }
}
