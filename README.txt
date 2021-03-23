================================================================
Erklährungen zur Erweiterung der MVVM-Anwendung mit der REST-Api
			Kurzfassung
================================================================

Eine View erstellen:
Diese in der jeweiligen Assembly
erstellen und erst danach unter die Views refaktorieren



----------------------------------------------------------------
Anwendung um ein Datenbank-Element
mit MVVM und der Rest-Api:

1. 1/2 Wir machen die Datenbank:
	ASP.NET eine .mdf Datei durch den Server-Explorer
	Benötigten Tabellen und ensprechende Prozedur
	
1. 2/2 Wir machen eine REST-API aus der Datenbank
	Config-Dateien anpassen
	Einen ApiController hinzufügen

2. Wir machen ein Transfer-Daten-Objekt:
	Eine Assembly mit dem Gebiet für die SqlController
	Eine Assembly für die Daten des Gebiets

3. Wir machen ein Model

	Fügen eine Aufgabe hinzu
	Füge eine View zu der Aufgabe hinzu
	Wir machen einen WebController, welcher den SqlController aufruft
