namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;


/// <summary>
/// Controller for working with 'Filialas' entity.
/// </summary>
public class FilialasController : Controller
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		//grąžinamas instruktorių sarašo vaizdas
        var filialai = FilialasRepo.List();
		return View(filialai);
	}

	/// <summary>
	/// This is invoked when creation form is first opened in browser.
	/// </summary>
	/// <returns>Creation form view.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var filialas = new FilialasCE();
        PopulateSelections(filialas);
		return View(filialas);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="darb">Entity model filled with latest data.</param>
	/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(FilialasCE fil)
	{
        var match = FilialasRepo.Find(fil.Model.Id);

		if(match != null) 
		{
			ModelState.AddModelError("id", "Field value already exists in database.");
		}
		//form field validation passed?
		if( ModelState.IsValid )
		{
			FilialasRepo.Insert(fil);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}

		//form field validation failed, go back to the form
		PopulateSelections(fil);
		return View(fil);
	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(int id)
	{
        var fililiasCE = FilialasRepo.Find(id);
        PopulateSelections(fililiasCE);
		return View(fililiasCE);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>		
	/// <param name="darb">Entity model filled with latest data.</param>
	/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(int id, FilialasCE fil)
	{
		//form field validation passed?
		if (ModelState.IsValid)
		{
			FilialasRepo.Update(fil);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}

		//form field validation failed, go back to the form
        PopulateSelections(fil);
		return View(fil);
	}

	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var fil = FilialasRepo.Find(id);
		return View(fil);
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
			FilialasRepo.Delete(id);

			//deletion success, redired to list form
			return RedirectToAction("Index");
		}
		//entity in use, deletion not permitted
		catch( MySql.Data.MySqlClient.MySqlException )
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;

			var fil = FilialasRepo.Find(id);
			return View("Delete", fil);
		}
	}

	/// <summary>
	/// Populates select lists used to render drop down controls.
	/// </summary>
	/// <param name="modelisEvm">'Automobilis' view model to append to.</param>
	public void PopulateSelections(FilialasCE filialas)
	{
		//load entities for the select lists
		var mokyklos = MokyklaRepo.List();

		//build select lists
		filialas.Lists.Mokyklos =
			mokyklos.Select(it => {
				return
					new SelectListItem() {
						Value = Convert.ToString(it.Kodas),
						Text = it.Pavadinimas
					};
			})
			.ToList();
	}
}
