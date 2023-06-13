namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;


/// <summary>
/// Database operations related to 'Administracijos darbuotojas' entity.
/// </summary>
public class DarbuotojasRepo
{
	public static List<DarbuotojasL> List()
	{
		var query = $@"SELECT 
							ad.tabelio_nr, ad.vardas, ad.pavarde, ad.telefono_nr, m.pavadinimas AS pavadinimas
					 FROM 
					 	`{Config.TblPrefix}administracijos_darbuotojai` ad
						LEFT JOIN `{Config.TblPrefix}vairavimo_mokyklos` m ON m.imones_kodas = ad.fk_VAIRAVIMO_MOKYKLA
					ORDER BY 
						ad.tabelio_nr ASC";
		var drc = Sql.Query(query);

		var result = 
			Sql.MapAll<DarbuotojasL>(drc, (dre, t) => {
				t.Tabelis = dre.From<int>("tabelio_nr");
				t.Vardas = dre.From<string>("vardas");
				t.Pavarde = dre.From<string>("pavarde");
				t.TelefonoNr = dre.From<string>("telefono_nr");
				t.fkVairavimoMokykla = dre.From<string>("pavadinimas");
			});

		return result;
	}

	public static List<DarbuotojasL> ListForFilialas(int id)
	{
		var query = $@"SELECT
							d.tabelio_nr, d.vardas, d.pavarde
						FROM `{Config.TblPrefix}administracijos_darbuotojai` d
						WHERE d.fk_FILIALAS = ?Id
						ORDER BY d.tabelio_nr ASC";

		var drc = Sql.Query(query, args => {
			args.Add("?Id", id);
		});

		var result = 
			Sql.MapAll<DarbuotojasL>(drc, (dre, t) => {
				t.Tabelis = dre.From<int>("tabelio_nr");
				t.Vardas = dre.From<string>("vardas");
				t.Pavarde = dre.From<string>("pavarde");
			});

		return result;
	}

    public static DarbuotojasCE Find(int Id)
    {
        var query = $@"SELECT * FROM `{Config.TblPrefix}administracijos_darbuotojai` WHERE tabelio_nr=?Id";

        var drc =
            Sql.Query(query, args =>
            {
                args.Add("?Id", Id);
            });

        if (drc.Count > 0)
        {
            var result =
                Sql.MapOne<DarbuotojasCE>(drc, (dre, t) =>
                {
                    t.Darbuotojas.Tabelis = dre.From<int>("tabelio_nr");
                    t.Darbuotojas.Vardas = dre.From<string>("vardas");
                    t.Darbuotojas.Pavarde = dre.From<string>("pavarde");
                    t.Darbuotojas.TelefonoNr = dre.From<string>("telefono_nr");
                    t.Darbuotojas.Elpastas = dre.From<string>("elpastas");
                    t.Darbuotojas.GimimoData = dre.From<DateTime>("gimimo_data");
                    t.Darbuotojas.fkVairavimoMokykla = dre.From<string>("fk_VAIRAVIMO_MOKYKLA");
                    t.Darbuotojas.fkFilialas = dre.From<int>("fk_FILIALAS");
                });

            return result;

		}

		return null;
    }

	public static void Update(DarbuotojasCE darb)
	{						
		var query = 
			$@"UPDATE `{Config.TblPrefix}administracijos_darbuotojai`
			SET 
				vardas=?vardas, 
				pavarde=?pavarde,
				telefono_nr=?telefonoNr,
				elpastas=?elPastas,
				gimimo_data=?gimimoData,
				fk_VAIRAVIMO_MOKYKLA=?fk_vairavimo_mokykla,
				fk_FILIALAS=?fk_filialas
			WHERE 
				tabelio_nr=?Nr";

		Sql.Update(query, args => {
			args.Add("?vardas", darb.Darbuotojas.Vardas);
			args.Add("?pavarde", darb.Darbuotojas.Pavarde);
			args.Add("?telefonoNr", darb.Darbuotojas.TelefonoNr);
			args.Add("?elPastas", darb.Darbuotojas.Elpastas);
			args.Add("?gimimoData", darb.Darbuotojas.GimimoData);
			args.Add("?fk_vairavimo_mokykla", darb.Darbuotojas.fkVairavimoMokykla);
			args.Add("?fk_filialas", darb.Darbuotojas.fkFilialas);
			args.Add("?Nr", darb.Darbuotojas.Tabelis);
		});				
	}

	public static void Insert(DarbuotojasCE darb)
	{							
		var query = 
			$@"INSERT INTO `{Config.TblPrefix}administracijos_darbuotojai`
			(
				tabelio_nr,
				vardas,
				pavarde,
				telefono_nr,
				elpastas,
				gimimo_data,
				fk_VAIRAVIMO_MOKYKLA,
				fk_FILIALAS
			)
			VALUES(
				?nr,
				?vardas,
				?pavarde,
				?telefonoNr,
				?elPastas,
				?gimimoData,
				?fk_vairavimo_mokykla,
				?fk_filialas
			)";

		Sql.Insert(query, args => {
			args.Add("?vardas", darb.Darbuotojas.Vardas);
			args.Add("?pavarde", darb.Darbuotojas.Pavarde);
			args.Add("?telefonoNr", darb.Darbuotojas.TelefonoNr);
			args.Add("?elPastas", darb.Darbuotojas.Elpastas);
			args.Add("?gimimoData", darb.Darbuotojas.GimimoData);
			args.Add("?fk_vairavimo_mokykla", darb.Darbuotojas.fkVairavimoMokykla);
			args.Add("?fk_filialas", darb.Darbuotojas.fkFilialas);
			args.Add("?Nr", darb.Darbuotojas.Tabelis);
		});				
	}

	public static void Delete(int Id)
	{			
		var query = $@"DELETE FROM `{Config.TblPrefix}administracijos_darbuotojai` WHERE tabelio_nr=?Id";
		Sql.Delete(query, args => {
			args.Add("?Id", Id);
		});			
	}
}
