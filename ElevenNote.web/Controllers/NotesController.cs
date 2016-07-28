﻿using ElevenNote.Model;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.web.Controllers
{
    [Authorize]

    public class NotesController : Controller
    {
        private readonly Lazy<NoteService> _svc;

        public NotesController()
        {
            //makes note service available to other projects
            _svc =
                 new Lazy<NoteService>(
                     () =>
                     {
                         var userId = Guid.Parse(User.Identity.GetUserId());
                         return new NoteService(userId);
                     });

        }
        // GET: Notes
        public ActionResult Index(string searchString)
        {
            var notes = _svc.Value.GetNotes();

            if (!String.IsNullOrEmpty(searchString))
            {
                notes = notes.Where(s => s.Title.Contains(searchString));
            }


            return View(notes);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]   //Applies to code below
        [ValidateAntiForgeryToken]

        public ActionResult Create(NoteCreateModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (!_svc.Value.CreateNote(model))
            {
                ModelState.AddModelError("", "Unable to create note");
                return View(model);
            }

            TempData["SaveResult"] = "Your note was created";

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var note = _svc.Value.GetNoteById(id);

            return View(note);
        }

        public ActionResult Edit(int id)
        {
            var note = _svc.Value.GetNoteById(id);
            var model =
                new NoteEditModel
                {
                    NoteId = note.NoteId,
                    Title = note.Title,
                    Content = note.Content,
                    IsStarred = note.IsStarred
                };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(NoteEditModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (!_svc.Value.UpdateNote(model))
            {
                ModelState.AddModelError("", "Unable to update note");
                return View(model);
            }

            TempData["SavedResult"] = "Your note was saved";

            return RedirectToAction("Index");
        }

        [ActionName("Delete")]

        public ActionResult DeleteGet(int id)
        {
            var detail = _svc.Value.GetNoteById(id);

            return View(detail);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult DeletePost(int id)
        {
            _svc.Value.DeleteNote(id);

            TempData["SavedResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }
    }
}