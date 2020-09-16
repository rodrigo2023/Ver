using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFinal2.Models;
using WebFinal2.Models.ViewModels;

namespace WebFinal2.Controllers
{
    public class TablaController : Controller
    {
        // GET: Tabla
        [Authorize]
        public ActionResult Index()
        {
            List<ListTablaViewModel> lst;

            using (CrudEntities db = new CrudEntities())
            {



                lst = (from d in db.operacion
                       orderby d.tipo ascending



                       select new ListTablaViewModel
                       {
                           id_operacion = d.id_operacion,
                           concepto = d.concepto,
                           monto = d.monto,
                           tipo=d.tipo,
                           


                       }).ToList();


            }


            return View(lst);
        }


        [Authorize]
        public ActionResult Nuevo()
        {

            return View();

        }


        [HttpPost]
        [Authorize]
        public ActionResult Nuevo(TablaViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    using (CrudEntities db = new CrudEntities())
                    {
                        var oTabla = new operacion();

                        oTabla.concepto = model.concepto;
                        oTabla.monto = model.monto;
                        oTabla.fecha_op = model.fecha_op;
                        oTabla.tipo = model.tipo;

                        db.operacion.Add(oTabla);
                        db.SaveChanges();

                    }

                    return Redirect("~/Tabla/");

                }

                return View(model);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }




        }

        [Authorize]
        public ActionResult Editar(int Id)
        {

            TablaViewModel model = new TablaViewModel();

            using (CrudEntities db = new CrudEntities())
            {
                var oTabla = db.operacion.Find(Id);
                model.concepto = oTabla.concepto;
                model.monto = oTabla.monto;
               
                model.id_operacion = oTabla.id_operacion;
            }


            return View(model);

        }


        [HttpPost]
        [Authorize]
        public ActionResult Editar(TablaViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    using (CrudEntities db = new CrudEntities())
                    {
                        var oTabla = db.operacion.Find(model.id_operacion);

                        oTabla.concepto = model.concepto;
                        oTabla.monto = model.monto;
                        oTabla.fecha_op = model.fecha_op;

                        db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                    }

                    return Redirect("~/Tabla/");

                }

                return View(model);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

        }



        [HttpGet]
        [Authorize]
        public ActionResult Eliminar(int Id)
        {



            using (CrudEntities db = new CrudEntities())
            {


                var oTabla = db.operacion.Find(Id);
                db.operacion.Remove(oTabla);
                db.SaveChanges();
            }


            return Redirect("~/Tabla/");
        }






    }

}