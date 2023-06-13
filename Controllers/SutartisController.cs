namespace Org.Ktu.Isk.P175B602.Autonuoma.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Newtonsoft.Json;

using Org.Ktu.Isk.P175B602.Autonuoma.Repositories;
using Org.Ktu.Isk.P175B602.Autonuoma.Models.Sutartis;


/// <summary>
/// Controller for working with 'Sutartis' entity. Implementation of F2 version.
/// </summary>
public class SutartisController : Controller
{
	/// <summary>
	/// This is invoked when either 'Index' action is requested or no action is provided.
	/// </summary>
	/// <returns>Entity list view.</returns>
	[HttpGet]
	public ActionResult Index()
	{
		return View(SutartisRepo.ListSutartis());
	}

	/// <summary>
	/// This is invoked when creation form is first opened in a browser.
	/// </summary>
	/// <returns>Entity creation form.</returns>
	[HttpGet]
	public ActionResult Create()
	{
		var sutCE = new SutartisCE();
		sutCE.Sutartis.Data = DateTime.Now;
		PopulateLists(sutCE);
		return View(sutCE);
	}


	/// <summary>
	/// This is invoked when buttons are pressed in the creation form.
	/// </summary>
	/// <param name="save">If not null, indicates that 'Save' button was clicked.</param>
	/// <param name="add">If not null, indicates that 'Add' button was clicked.</param>
	/// <param name="remove">If not null, indicates that 'Remove' button was clicked and contains in-list-id of the item to remove.</param>
	/// <param name="sutCE">Entity view model filled with latest data.</param>
	/// <returns>Returns creation from view or redirets back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Create(SutartisCE sutCE)
	{
		if(ModelState.IsValid)
        {
            SutartisRepo.Insert(sutCE);

            return RedirectToAction("Index");
        }

		PopulateLists(sutCE);
		return View(sutCE);
		
	}

	/// <summary>
	/// This is invoked when editing form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to edit.</param>
	/// <returns>Editing form view.</returns>
	[HttpGet]
	public ActionResult Edit(int id)
	{
		var sutCE = SutartisRepo.Find(id);	
		PopulateLists(sutCE);

		return View(sutCE);
	}

	/// <summary>
	/// This is invoked when buttons are pressed in the editing form.
	/// </summary>
	/// <param name="id">ID of the entity being edited</param>
	/// <param name="save">If not null, indicates that 'Save' button was clicked.</param>
	/// <param name="add">If not null, indicates that 'Add' button was clicked.</param>
	/// <param name="remove">If not null, indicates that 'Remove' button was clicked and contains in-list-id of the item to remove.</param>
	/// <param name="sutCE">Entity view model filled with latest data.</param>
	/// <returns>Returns editing from view or redired back to Index if save is successfull.</returns>
	[HttpPost]
	public ActionResult Edit(int id, SutartisCE sutCE)
	{
        if(ModelState.IsValid)
        {
            SutartisRepo.Update(sutCE);

            return RedirectToAction("Index");
        }

        PopulateLists(sutCE);
        return View(sutCE);
	}

	/// <summary>
	/// This is invoked when deletion form is first opened in browser.
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view.</returns>
	[HttpGet]
	public ActionResult Delete(int id)
	{
		var sutCE = SutartisRepo.Find(id);
		return View(sutCE);
	}

	/// <summary>
	/// This is invoked when deletion is confirmed in deletion form
	/// </summary>
	/// <param name="id">ID of the entity to delete.</param>
	/// <returns>Deletion form view on error, redirects to Index on success.</returns>
	[HttpPost]
	public ActionResult DeleteConfirm(int id)
	{
		//load 'Sutartis'
		var sutCE = SutartisRepo.Find(id);

		//'Sutartis' is in the state where deletion is permitted?
		if( sutCE.Sutartis.Busena == 3 )
		{
			//delete the entity
			SutartisRepo.Delete(id);

			//redired to list form
			return RedirectToAction("Index");
		}
		//'Sutartis' is in state where deletion is not permitted
		else
		{
			//enable explanatory message and show delete form
			ViewData["deletionNotPermitted"] = true;
			return View("Delete", sutCE);
		}
	}

	/// <summary>
	/// Populates select lists used to render drop down controls.
	/// </summary>
	/// <param name="sutCE">'Sutartis' view model to append to.</param>
	private void PopulateLists(SutartisCE sutCE)
	{
		//load entities for the select lists
		var automobiliai = AutomobilisRepo.ListForFilialas(sutCE.Sutartis.FkFilialas);
		var busenos = SutartisRepo.ListSutartiesBusena();
		var instruktoriai = InstruktoriusRepo.ListForFilialas(sutCE.Sutartis.FkFilialas);
		var klientai = KlientasRepo.List();
        var darbuotojai = DarbuotojasRepo.ListForFilialas(sutCE.Sutartis.FkFilialas);
        var paslaugos = PaslaugaRepo.List();

		//build select lists
		sutCE.Lists.Automobiliai =
			automobiliai
				.Select(it =>
				{
					return
						new SelectListItem
						{
							Value = Convert.ToString(it.VinNr),
							Text = $"{it.ValstybinisNr} - {it.Marke} {it.Modelis}"
						};
				})
				.ToList();

		sutCE.Lists.Busenos =
			busenos
				.Select(it =>
				{
					return
						new SelectListItem
						{
							Value = Convert.ToString(it.Id),
							Text = it.Name
						};
				})
				.ToList();

		sutCE.Lists.Instruktoriai =
			instruktoriai
				.Select(it =>
				{
					return
						new SelectListItem
						{
							Value = Convert.ToString(it.Id),
							Text = $"{it.Vardas} {it.Pavarde} - {it.Id}"
						};
				})
				.ToList();

		sutCE.Lists.Klientai =
			klientai
				.Select(it =>
				{
					return
						new SelectListItem
						{
							Value = it.AsmensKodas,
							Text = $"{it.Vardas} {it.Pavarde} - {it.AsmensKodas}"
						};
				})
				.ToList();	

        sutCE.Lists.Darbuotojai =
            darbuotojai
                .Select(it => 
                {
                    return 
                        new SelectListItem
                        {
                            Value = Convert.ToString(it.Tabelis),
                            Text = $"{it.Vardas} {it.Pavarde}"
                        };
                })	
                .ToList();

        sutCE.Lists.Paslaugos = 
            paslaugos 
                .Select(it => 
                {
                    return 
                        new SelectListItem
                        {
                            Value = Convert.ToString(it.Id),
                            Text = it.Pavadinimas
                        };
                })
                .ToList();


        {
			//initialize the destination list
			sutCE.Lists.Filialai = new List<SelectListItem>();

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
					sutCE.Lists.Filialai.Add(sle);
				}
			}
		}		
		
	}
}
