using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
        public ActionResult Create([Bind(Include = "Nome,Esquadra")] Agentes agente,HttpPostedFileBase fileUploadFotografia
            )
        {
            //determinar o ID do agente

            int novoID = db.Agentes.Max(a => a.ID) + 1;

            //atribuir o id ao novo agente
            agente.ID = novoID;

            //var auxiliar
            string nomeFotografia = "Agente " + novoID + ".jpg";
            string caminhoParaFotografia = Path.Combine(Server.MapPath("~/imagens/"),nomeFotografia);

            //escrever a fotografia no disco rigido 
            //verificar se chega efetivamente um ficheiro ao servidor
            if (fileUploadFotografia != null)
            {
                //guardar o nome da imagem na bd
                agente.Fotografia = nomeFotografia;


            }
            else //nao ha imagem 
            {
                ModelState.AddModelError("", "Não foi fornecida uma imagem...");
                return View(agente);//reenvia os dados do agente para a view
            }


            //ModelState.IsValid --> confronta os dados fornecidos com o modelo
            // se nao respeitar as regras do modelo rejeita os dados
            if (ModelState.IsValid)
            {
                //adiciona na estrutura de dados ,na memoria do servidor,
                //o obejto agentes
                db.Agentes.Add(agente);
                //redireciona o utilizador oara a pagina de inicio

                db.SaveChanges();
                //guardar a imagem no disco rigido 
                fileUploadFotografia.SaveAs(caminhoParaFotografia);

                return RedirectToAction("Index");
            }

            return View(agente);
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
