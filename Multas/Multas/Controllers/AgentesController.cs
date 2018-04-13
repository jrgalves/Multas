﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Multas.Models;

namespace Multas.Controllers
{
    public class AgentesController : Controller
    {
        //cria um objeto privado que representa a base de dados
        private MultasDb db = new MultasDb();

        // GET: Agentes
        public ActionResult Index()
        {
            //(link)db.agente.tolist() --> em sql :select * from agentes
            //constroi uma lista com os dados de todos os agentes 
            //e envia-a para a view
            return View(db.Agentes.ToList());
        }

        // GET: Agentes/Details/5
        /// <summary>
        /// apresenta os detalhes de um agente
        /// </summary>
        /// <param name="id">representa a pk que identifica o agente</param>
        /// <returns></returns>
        /// 
        //int? significa que pode haver valores nulos
        public ActionResult Details(int? id)
        {
            //protege a execuçao do metodo contra a nao existencia de dados
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //vai procurar o gante cujo id fi fornecido 
            Agentes agentes = db.Agentes.Find(id);

            //se o agente nao for encontrado...
            if (agentes == null)
            {
                return HttpNotFound();
            }

            //envia para a view os dados do agente
            return View(agentes);
        }

        // GET: Agentes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //protege o metodo contra ataques de roubo de identidade
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,Fotografia,Esquadra")] Agentes agentes)
        {
            //ModelState.IsValid --> confronta os dados fornecidos com o modelo
            // se nao respeitar as regras do modelo rejeita os dados
            if (ModelState.IsValid)
            {
                //adiciona na estrutura de dados ,na memoria do servidor,
                //o obejto agentes
                db.Agentes.Add(agentes);
                //redireciona o utilizador oara a pagina de inicio

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agentes);
        }

        // GET: Agentes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                return HttpNotFound();
            }
            return View(agentes);
        }

        // POST: Agentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Fotografia,Esquadra")] Agentes agentes)
        {
            if (ModelState.IsValid)
            {
                //atuaçiza os dados do agente na estrura de dados em memoria
                db.Entry(agentes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agentes);
        }

        // GET: Agentes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                return HttpNotFound();
            }
            return View(agentes);
        }

        // POST: Agentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //procurar agente
            Agentes agentes = db.Agentes.Find(id);
            //remover da memoria
            db.Agentes.Remove(agentes);
            //commit na bd
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
