namespace Org.Ktu.Isk.P175B602.Autonuoma.Repositories;

using MySql.Data.MySqlClient;

using Branchreport = Org.Ktu.Isk.P175B602.Autonuoma.Models.BranchReport;


/// <summary>
/// Database operations related to reports.
/// </summary>
public class AtaskaitaRepo
{
	public static List<Branchreport.BranchReport> GetBranchReport(DateTime? dateFrom, DateTime? dateTo, string fAdresas, string imonesPavadinimas) 
	{
		var query =
			$@"SELECT
				fil.id,
				fil.adresas,
				fil.elpastas,
				vm.pavadinimas,
				p.pavadinimas AS paslaugos_pavadinimas,
				COUNT(sut.nr) AS kiekis,
				SUM(sut.kaina) AS suma,
				fd.kiekis_filiale,
				fd.suma_filiale
			FROM
				`{Config.TblPrefix}filialai` fil
				LEFT JOIN `{Config.TblPrefix}vairavimo_mokyklos` vm ON vm.imones_kodas = fil.fk_VAIRAVIMO_MOKYKLA
				INNER JOIN `{Config.TblPrefix}sutartys` sut ON sut.fk_FILIALAS = fil.id
				LEFT JOIN `{Config.TblPrefix}paslaugos` p ON sut.fk_PASLAUGA = p.id		
				LEFT JOIN 
					(
						SELECT 
							f.id,
							COUNT(s.fk_PASLAUGA) AS kiekis_filiale,
							SUM(s.kaina) AS suma_filiale
						FROM
							`{Config.TblPrefix}filialai` f
						LEFT JOIN
							`{Config.TblPrefix}sutartys` s ON s.fk_FILIALAS = f.id 
						WHERE				
							s.sudarymo_data >= IFNULL(?nuo, s.sudarymo_data)
							AND s.sudarymo_data <= IFNULL(?iki, s.sudarymo_data)
						GROUP BY
						f.id
					) AS fd 
					ON fd.id = fil.id
				WHERE
					(?fAdresas IS NULL OR fil.adresas LIKE CONCAT('%', ?fAdresas, '%'))
					AND (?imonesPavadinimas IS NULL OR vm.pavadinimas LIKE CONCAT('%', ?imonesPavadinimas, '%'))
    				AND (sut.sudarymo_data >= IFNULL(?nuo, sut.sudarymo_data))
    				AND (sut.sudarymo_data <= IFNULL(?iki, sut.sudarymo_data))
				GROUP BY
					fil.adresas,
					fil.elpastas,
					vm.pavadinimas,
					p.pavadinimas
				ORDER BY
					fil.Id ASC";

		var drc = 
			Sql.Query(query, args => {
				args.Add("?nuo", dateFrom);
				args.Add("?iki", dateTo);
				args.Add("?fAdresas", fAdresas);
				args.Add("?imonesPavadinimas", imonesPavadinimas);
			});

		

		var result = 
			Sql.MapAll<Branchreport.BranchReport>(drc, (dre, t) => {
				t.Id = dre.From<int>("id");
				t.fAdresas = dre.From<string>("adresas");
				t.elPastas = dre.From<string>("elpastas");
				t.imonesPavadinimas = dre.From<string>("pavadinimas");
				t.paslaugosPavadinimas = dre.From<string>("paslaugos_pavadinimas");
				t.Kiekis = dre.From<int>("kiekis");
				t.PaslauguKaina = dre.From<decimal>("suma");
				t.BendrasKiekis = dre.From<int>("kiekis_filiale");
				t.BendraSumaSutarciu = dre.From<decimal>("suma_filiale");

			});

		return result;

	}

}
