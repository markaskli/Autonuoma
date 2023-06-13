namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


/// <summary>
/// Controller for working with 'Instruktorius' entity.
/// </summary>
public class InstruktoriusController : Controller
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		return View(InstruktoriusRepo.List());
	}

	/// <summary>
	/// This is invoked when creation form is first opened in browser.
	/// </summary>
	/// <returns>Creation form view.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var inst = new InstruktoriusCE();
		PopulateSelections(inst);
		return View(inst);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="darb">Entity model filled with latest data.</param>
	/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(InstruktoriusCE inst)
	{
		var match = InstruktoriusRepo.Find(inst.Instruktorius.Id);

		if(match != null) 
		{
			ModelState.AddModelError("id", "Field value already exists in database.");
		}

		if (ModelState.IsValid)
		{
			//NOTE: insert will fail if someone managed to add different 'darbuotojas' with same 'tabelis' after the check
			InstruktoriusRepo.Insert(inst);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}
		
		//form field validation failed, go back to the form
		PopulateSelections(inst);
		return View(inst);
	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(int id)
	{
		var instCE = InstruktoriusRepo.Find(id);
		PopulateSelections(instCE);
		return View(instCE);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>		
	/// <param name="darb">Entity model filled with latest data.</param>
	/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(string id, InstruktoriusCE inst)
	{
		//form field validation passed?
		if (ModelState.IsValid)
		{
			InstruktoriusRepo.Update(inst);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}

		//form field validation failed, go back to the form
		PopulateSelections(inst);
		return View(inst);
	}

	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var inst = InstruktoriusRepo.Find(id);
		return View(inst);
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
			InstruktoriusRepo.Delete(id);

			//deletion success, redired to list form
			return RedirectToAction("Index");
		}
		//entity in use, deletion not permitted
		catch( MySql.Data.MySqlClient.MySqlException )
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;

			var inst = InstruktoriusRepo.Find(id);
			PopulateSelections(inst);

			return View("Delete", inst);
		}
	}

    /// <summary>
    /// Populates select lists used to render drop down controls.
    /// </summary>
    /// <param name="modelisEvm">'Automobilis' view model to append to.</param>
    public void PopulateSelections(InstruktoriusCE inst)
    {
        {
			//initialize the destination list
			inst.Lists.Filialai = new List<SelectListItem>();

			//load 'Marke' entities to use for item groups
			var mokyklos = MokyklaRepo.List();

			//create select list items from 'Modelis' related to each 'Marke'
			foreach( var mokykla in mokyklos )
			{
				//create list item group for current 'Mokykla' entity
				var itemGrp = new SelectListGroup() { Name = mokykla.Pavadinimas  };

				//load related 'Modelis' entities
				var filialai = FilialasRepo.ListForMokykla(mokykla.Kodas);

				//build list items for the group
				foreach( var filialas in filialai ) 
				{
					var sle =
						new SelectListItem {
							Value = Convert.ToString(filialas.Id),
							Text = filialas.Adresas,
							Group = itemGrp
						};
					inst.Lists.Filialai.Add(sle);
				}
			}
		}
        
    }
}
