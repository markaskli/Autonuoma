namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models;


/// <summary>
/// Controller for working with 'Modelis' entity.
/// </summary>
public class SaskaitaController : Controller
{

	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		var saskaitos = SaskaitaRepo.List();
		return View(saskaitos);
	}

	/// <summary>
	/// This is invoked when creation form is first opened in browser.
	/// </summary>
	/// <returns>Creation form view.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var saskaitaCE = new SaskaitaCE();
		PopulateSelections(saskaitaCE);
		return View(saskaitaCE);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="saskCE">Entity model filled with latest data.</param>
	/// <returns>Returns creation from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(SaskaitaCE saskCE)
	{
		var match = SaskaitaRepo.FindSaskaitaCE(saskCE.Model.Nr);
		if(match != null) 
		{
			ModelState.AddModelError("nr", "Field value already exists in database.");
		}
		//form field validation passed?
		if( ModelState.IsValid )
		{
			SaskaitaRepo.InsertSaskaita(saskCE);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}

		//form field validation failed, go back to the form
		PopulateSelections(saskCE);
		return View(saskCE);
	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(int id)
	{
		var saskaitaCE = SaskaitaRepo.FindSaskaitaCE(id);
		PopulateSelections(saskaitaCE);

		return View(saskaitaCE);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>
	/// <param name="autoEvm">Entity model filled with latest data.</param>
	/// <returns>Returns editing from view or redirects back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(int id, SaskaitaCE saskaitaCE)
	{
		//form field validation passed?
		if( ModelState.IsValid )
		{
			SaskaitaRepo.UpdateSaskaita(saskaitaCE);

			//save success, go back to the entity list
			return RedirectToAction("Index");
		}

		//form field validation failed, go back to the form
		PopulateSelections(saskaitaCE);
		return View(saskaitaCE);
	}

	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var saskaita = SaskaitaRepo.FindSaskaitaCE(id);
		return View(saskaita);
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
			SaskaitaRepo.DeleteSaskaita(id);

			//deletion success, redired to list form
			return RedirectToAction("Index");
		}
		//entity in use, deletion not permitted
		catch( MySql.Data.MySqlClient.MySqlException )
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;

			var saskaita = SaskaitaRepo.FindSaskaitaCE(id);
            PopulateSelections(saskaita);

			return View("Delete", saskaita);
		}
	}

    /// <summary>
    /// Populates select lists used to render drop down controls.
    /// </summary>
    /// <param name="saskCE"></param>
    public void PopulateSelections(SaskaitaCE saskCE) 
    {

		var sutartys = SutartisRepo.ListSutartis();

		//build select lists
		saskCE.Lists.Sutartys =
			sutartys.Select(it => {
				return
					new SelectListItem() {
						Value = Convert.ToString(it.Nr),
						Text = $"{Convert.ToString(it.Nr)} - {it.FkKlientas}"
					};
			})
			.ToList();
    
    }
}
