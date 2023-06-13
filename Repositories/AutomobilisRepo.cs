namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models.Automobilis;


/// <summary>
/// Database operations related to 'Automobilis'.
/// </summary>
public class AutomobilisRepo
{
	public static List<AutomobilisL> ListAutomobilis()
	{
		var query =
			$@"SELECT
				a.VIN_nr,
				a.valstybinis_nr,
				m.pavadinimas AS modelis,
				mm.pavadinimas AS marke,
				f.adresas AS filialas,
				vm.pavadinimas AS mokykla
			FROM
				{Config.TblPrefix}automobiliai a
				LEFT JOIN `{Config.TblPrefix}modeliai` m ON m.id = a.fk_MODELIS
				LEFT JOIN `{Config.TblPrefix}markes` mm ON mm.id = m.fk_MARKE
				LEFT JOIN `{Config.TblPrefix}filialai` f ON f.id = a.fk_FILIALAS
				LEFT JOIN `{Config.TblPrefix}vairavimo_mokyklos` vm ON vm.imones_kodas = f.fk_VAIRAVIMO_MOKYKLA
			ORDER BY a.VIN_nr ASC";

		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<AutomobilisL>(drc, (dre, t) => {
				t.VinNr = dre.From<string>("VIN_nr");
				t.ValstybinisNr = dre.From<string>("valstybinis_nr");
				t.Modelis = dre.From<string>("modelis");
				t.Marke = dre.From<string>("marke");
				t.FkFilialas = dre.From<string>("filialas");
				t.Mokykla = dre.From<string>("mokykla");
			});

		return result;
	}

	public static List<AutomobilisL> ListForFilialas(int id)
	{
		var query = $@"SELECT
							a.VIN_nr, a.valstybinis_nr, m.pavadinimas AS modelis, mm.pavadinimas AS marke
						FROM 
							{Config.TblPrefix}automobiliai a 
							LEFT JOIN {Config.TblPrefix}modeliai m ON m.id = a.fk_MODELIS
							LEFT JOIN {Config.TblPrefix}markes mm ON mm.id = m.fk_MARKE
						WHERE a.fk_FILIALAS = ?Id
						ORDER BY a.VIN_nr ASC";

		var drc = Sql.Query(query, args => {
			args.Add("?Id", id);
		});

        var result =
            Sql.MapAll<AutomobilisL>(drc, (dre, t) =>
            {
                t.VinNr = dre.From<string>("VIN_nr");
                t.ValstybinisNr = dre.From<string>("valstybinis_nr");
                t.Modelis = dre.From<string>("modelis");
                t.Marke = dre.From<string>("marke");
            });

        return result;
	}

	public static AutomobilisCE FindAutomobolisCE(string vin_nr)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}automobiliai` WHERE VIN_nr=?vin_nr";

		var drc =
			Sql.Query(query, args => {
				args.Add("?vin_nr", vin_nr);
			});

		if(drc.Count > 0) {
			var result = 
				Sql.MapOne<AutomobilisCE>(drc, (dre, t) => {
					var auto = t.Automobilis;
				//
					auto.VinNr = dre.From<string>("VIN_nr");
					auto.ValstybinisNr = dre.From<string>("valstybinis_nr");
					auto.PagaminimoData = dre.From<DateTime>("pagaminimo_data");
					auto.Rida = dre.From<int>("rida");
					auto.RegistravimoData = dre.From<DateTime>("registravimo_data");
					auto.Verte = dre.From<decimal>("verte");
					auto.FkPavaruDeze = dre.From<int>("pavaru_deze");
					auto.FkKuroTipas = dre.From<int>("kuro_tipas");
					auto.FkKebuloTipas = dre.From<int>("kebulas");
					auto.FkModelis = dre.From<int>("fk_MODELIS");
					auto.FkFilialas = dre.From<int>("fk_FILIALAS");
				});

				return result;
		}

		return null;
	}

	public static void InsertAutomobilis(AutomobilisCE autoCE)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}automobiliai`
			(
				`VIN_nr`,
				`valstybinis_nr`,
				`pagaminimo_data`,
				`rida`,
				`registravimo_data`,
				`verte`,
				`pavaru_deze`,
				`kuro_tipas`,
				`kebulas`,
				`fk_MODELIS`,
				`fk_FILIALAS`
			)
			VALUES (
				?vin_nr,
				?vlst_nr,
				?pag_data,
				?rida,
				?reg_dt,
				?verte,
				?pav_deze,
				?kuro_tip,
				?kebulas,
				?fk_mod,
				?fk_filialas
			)";

		Sql.Insert(query, args => {
			//make a shortcut
			var auto = autoCE.Automobilis;

			//
			args.Add("?vin_nr", auto.VinNr);
			args.Add("?vlst_nr", auto.ValstybinisNr);
			args.Add("?pag_data", auto.PagaminimoData?.ToString("yyyy-MM-dd"));
			args.Add("?rida", auto.Rida);
			args.Add("?reg_dt", auto.RegistravimoData?.ToString("yyyy-MM-dd"));
			args.Add("?verte", auto.Verte);
			args.Add("?pav_deze", auto.FkPavaruDeze);
			args.Add("?kuro_tip", auto.FkKuroTipas);
			args.Add("?kebulas", auto.FkKebuloTipas);
			args.Add("?fk_mod", auto.FkModelis);
			args.Add("?fk_filialas", auto.FkFilialas);
		});
	}

	public static void UpdateAutomobilis(AutomobilisCE autoCE)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}automobiliai`
			SET
				`valstybinis_nr` = ?vlst_nr,
				`pagaminimo_data` = ?pag_data,
				`rida` = ?rida,
				`registravimo_data` = ?reg_dt,
				`verte` = ?verte,
				`pavaru_deze` = ?pav_deze,
				`kuro_tipas` = ?kuro_tip,
				`kebulas` = ?kebulas,
				`fk_MODELIS` = ?fk_mod,
				`fk_FILIALAS` = ?fk_filialas
			WHERE
				VIN_nr=?vin_nr";

		Sql.Update(query, args => {
			//make a shortcut
			var auto = autoCE.Automobilis;

			//
			args.Add("?vin_nr", auto.VinNr);
			args.Add("?vlst_nr", auto.ValstybinisNr);
			args.Add("?pag_data", auto.PagaminimoData?.ToString("yyyy-MM-dd"));
			args.Add("?rida", auto.Rida);
			args.Add("?reg_dt", auto.RegistravimoData?.ToString("yyyy-MM-dd"));
			args.Add("?verte", auto.Verte);
			args.Add("?pav_deze", auto.FkPavaruDeze);
			args.Add("?kuro_tip", auto.FkKuroTipas);
			args.Add("?kebulas", auto.FkKebuloTipas);
			args.Add("?fk_mod", auto.FkModelis);
			args.Add("?fk_filialas", auto.FkFilialas);
		});
	}

	public static void DeleteAutomobilis(string vin_nr)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}automobiliai` WHERE VIN_nr=?vin_nr";
		Sql.Delete(query, args => {
			args.Add("?vin_nr", vin_nr);
		});
	}

	public static List<PavaruDeze> ListPavaruDeze()
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}pavaru_dezes` ORDER BY id ASC";
		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<PavaruDeze>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Pavadinimas = dre.From<string>("name");
			});

		return result;
	}

	public static List<KebuloTipas> ListKebuloTipas()
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}kebulo_tipai` ORDER BY id ASC";
		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<KebuloTipas>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Pavadinimas = dre.From<string>("name");
			});

		return result;
	}

	public static List<KuroTipas> ListKuroTipas()
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}kuro_tipai` ORDER BY id ASC";
		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<KuroTipas>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Pavadinimas = dre.From<string>("name");
			});

		return result;
	}

}
