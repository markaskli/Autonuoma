namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;


/// <summary>
/// Controller for working with 'Modelis' entity.
/// </summary>
public class MokejimasController : Controller
{

	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		var mokejimai = MokejimasRepo.List();
		return View(mokejimai);
	}

	/// <summary>
	/// This is invoked when creation form is first opened in browser.
	/// </summary>
	/// <returns>Creation form view.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var mokejimasCE = new MokejimasCE();
		PopulateSelections(mokejimasCE);
		return View(mokejimasCE);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="saskCE">Entity model filled with latest data.</param>
	/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(MokejimasCE mokCE)
	{
		//form field validation passed?
		if( ModelState.IsValid )
		{
			MokejimasRepo.Insert(mokCE);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}

		//form field validation failed, go back to the form
		PopulateSelections(mokCE);
		return View(mokCE);
	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(int id)
	{
		var mokejimasCE = MokejimasRepo.Find(id);
		PopulateSelections(mokejimasCE);

		return View(mokejimasCE);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>
	/// <param name="autoEvm">Entity model filled with latest data.</param>
	/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(int id, MokejimasCE mokCE)
	{
		//form field validation passed?
		if( ModelState.IsValid )
		{
			MokejimasRepo.Update(mokCE);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}

		//form field validation failed, go back to the form
		PopulateSelections(mokCE);
		return View(mokCE);
	}

	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var mokejimas = MokejimasRepo.Find(id);
		return View(mokejimas);
	}

	/// <summary>
	/// This is invoked when deletion is confirmed in deletion form
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view on error, redirects to Index on success.</returns>
	[HttpPost]
	public ActionResult DeleteConfirm(int id)
	{
		//try deleting, this will fail if foreign key constraint fails
		try
		{
			MokejimasRepo.Delete(id);

			//deletion success, redired to list form
			return RedirectToAction("Index");
		}
		//entity in use, deletion not permitted
		catch( MySql.Data.MySqlClient.MySqlException )
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;

			var mok = MokejimasRepo.Find(id);
            PopulateSelections(mok);

			return View("Delete", mok);
		}
	}

    /// <summary>
    /// Populates select lists used to render drop down controls.
    /// </summary>
    /// <param name="saskCE"></param>
    public void PopulateSelections(MokejimasCE mokejimasCE) 
    {

		var mokejimoTipai = MokejimasRepo.ListAtsiskaitymoTipas();
		var klientai = KlientasRepo.List();
		var saskaitos = SaskaitaRepo.ListForKlientas(mokejimasCE.Mokejimas.fk_KLIENTAS);

		mokejimasCE.Lists.AtsiskaitymoTipai = 
			mokejimoTipai.Select(it =>  {
				return 
					new SelectListItem() {
						Value = Convert.ToString(it.Id),
						Text = it.Pavadinimas
					};
			})
			.ToList();   


		mokejimasCE.Lists.Klientai = 
			klientai.Select(it => {
				return 
					new SelectListItem() {
						Value = it.AsmensKodas,
						Text = it.AsmensKodas
					};
			})
			.ToList();

		mokejimasCE.Lists.Saskaitos = 
			saskaitos.Select(it => {
				return 
					new SelectListItem() {
						Value = Convert.ToString(it.Nr),
						Text = Convert.ToString(it.Nr)
					};
			})
			.ToList();
		


			
    
    }
}
