namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models.Mokykla;


/// <summary>
/// Controller for working with 'Vairavimo mokykla' entity.
/// </summary>
public class MokyklaController : Controller
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		return View(MokyklaRepo.List());
	}

	/// <summary>
	/// This is invoked when creation form is first opened in browser.
	/// </summary>
	/// <returns>Creation form view.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var mokyklaCE = new MokyklaCE();
		return View(mokyklaCE);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="mokyklaCE">Entity model filled with latest data.</param>
	/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(MokyklaCE mokyklaCE)
	{
		//do not allow creation of entity with 'MokyklaKodas' field matching existing one
		var match = MokyklaRepo.Find(mokyklaCE.Mokykla.Kodas);

		if( match !=null )
			ModelState.AddModelError("Kodas", "Field value already exists in database.");

		//form field validation passed?
		if( ModelState.IsValid )
		{
			MokyklaRepo.Insert(mokyklaCE);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}
		
		//form field validation failed, go back to the form
		return View(mokyklaCE);
	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(string id)
	{
		return View(MokyklaRepo.Find(id));
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>		
	/// <param name="mokyklaCE">Entity model filled with latest data.</param>
	/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(string id, MokyklaCE mokyklaCE)
	{
		//form field validation passed?
		if (ModelState.IsValid)
		{
			MokyklaRepo.Update(mokyklaCE);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}

		//form field validation failed, go back to the form
		return View(mokyklaCE);
	}

	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(string id)
	{
		var mokykla = MokyklaRepo.Find(id);
		return View(mokykla);
	}

	/// <summary>
	/// This is invoked when deletion is confirmed in deletion form
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view on error, redirects to Index on success.</returns>
	[HttpPost]
	public ActionResult DeleteConfirm(string id)
	{
		//try deleting, this will fail if foreign key constraint fails
		try 
		{
			MokyklaRepo.Delete(id);

			//deletion success, redired to list form
			return RedirectToAction("Index");
		}
		//entity in use, deletion not permitted
		catch( MySql.Data.MySqlClient.MySqlException )
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;

			var mokykla = MokyklaRepo.Find(id);
			return View("Delete", mokykla);
		}
	}
}
