namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Org.Ktu.Isk.P175B602.Autonuoma.Models;


/// <summary>
/// Database operations related to 'Automobilis'.
/// </summary>
public class MokejimasRepo
{
	public static List<MokejimasL> List() 
	{
		var query =
			$@"SELECT
				m.id,
				m.data,
				m.suma,
				s.nr AS saskaita,
				k.asmens_kodas AS klientas
			FROM
				{Config.TblPrefix}mokejimai m
				LEFT JOIN `{Config.TblPrefix}saskaitos` s ON s.nr = m.fk_SASKAITA
				LEFT JOIN `{Config.TblPrefix}klientai` k ON k.asmens_kodas = m.fk_KLIENTAS
			ORDER BY m.id ASC";

		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<MokejimasL>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Data = dre.From<DateTime>("data");
                t.Suma = dre.From<decimal>("suma");
                t.fk_SASKAITA = dre.From<int>("saskaita");
				t.fk_KLIENTAS = dre.From<string>("klientas");
			});

		return result;
	}

	public static MokejimasCE Find(int Id)
	{
		var query = $@"SELECT * FROM `{Config.TblPrefix}mokejimai` WHERE id=?Id";

		var drc =
			Sql.Query(query, args => {
				args.Add("?Id", Id);
			});

		var result =
			Sql.MapOne<MokejimasCE>(drc, (dre, t) => {
				//make a shortcut
				var mokejimas = t.Mokejimas;

				//
                mokejimas.Data = dre.From<DateTime>("data");
                mokejimas.Suma = dre.From<decimal>("suma");
                mokejimas.Tipas = dre.From<int>("atsiskaitymo_tipas");
                mokejimas.Id = dre.From<int>("id");
                mokejimas.fk_KLIENTAS = dre.From<string>("fk_KLIENTAS");
                mokejimas.fk_SASKAITA = dre.From<int>("fk_SASKAITA");

			});

		return result;
	}

	public static void Insert(MokejimasCE mokCE)
	{
		var query =
			$@"INSERT INTO `{Config.TblPrefix}mokejimai`
			(
				`data`,
                `suma`,
                `atsiskaitymo_tipas`,
                `id`,
                `fk_KLIENTAS`,
                `fk_SASKAITA`
			)
			VALUES (				
				?Data,
				?Suma,
                ?Atsiskaitymo_tipas,
                ?Id,
				?fk_klientas,
                ?fk_saskaita
			)";

		Sql.Insert(query, args => {
			//make a shortcut
			var mok = mokCE.Mokejimas;

			//
            args.Add("?Data", mok.Data);
            args.Add("?Suma", mok.Suma);
            args.Add("?Atsiskaitymo_tipas", mok.Tipas);
            args.Add("?Id", mok.Id);
            args.Add("?fk_klientas", mok.fk_KLIENTAS);
            args.Add("?fk_saskaita", mok.fk_SASKAITA);
		});
	}

	public static void Update(MokejimasCE mokCE)
	{
		var query =
			$@"UPDATE `{Config.TblPrefix}mokejimai`
			SET
				`data` = ?Data,
				`suma` = ?Suma,
                `atsiskaitymo_tipas` = ?Atsiskaitymo_tipas,
                `fk_KLIENTAS` = ?fk_klientas,
				`fk_SASKAITA` = ?fk_saskaita
			WHERE
				id=?Id";

		Sql.Update(query, args => {
			//make a shortcut
			var mok = mokCE.Mokejimas;

			//
            args.Add("?Data", mok.Data);
            args.Add("?Suma", mok.Suma);
            args.Add("?Atsiskaitymo_tipas", mok.Tipas);
            args.Add("?fk_klientas", mok.fk_KLIENTAS);
            args.Add("?fk_saskaita", mok.fk_SASKAITA);

            args.Add("?Id", mok.Id);
		});
	}

	public static void Delete(int Id)
	{
		var query = $@"DELETE FROM `{Config.TblPrefix}mokejimai` WHERE id=?Id";
		Sql.Delete(query, args => {
			args.Add("?Id", Id);
		});
	}

    public static List<AtsiskaitymoTipas> ListAtsiskaitymoTipas() 
    {
		var query = $@"SELECT * FROM `{Config.TblPrefix}atsiskaitymo_tipai` ORDER BY id ASC";
		var drc = Sql.Query(query);

		var result =
			Sql.MapAll<AtsiskaitymoTipas>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.Pavadinimas = dre.From<string>("name");
			});

		return result;
    }

}
