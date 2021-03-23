================================================================
Erklährungen zur Erweiterung der MVVM-Anwendung mit der REST-Api
			Kurzfassung
================================================================

Eine View erstellen:
Zum erstellen einer View diese in der jeweiligen Assembly
erstellen und erst danach unter die Views refaktorieren

Anwendung um ein Datenbank-Element e
mit MVVM und der Rest-Api:

1. Wir machen die Datenbank
	ASP.NET
	Erstelle eine .mdf Datei durch den Server-Explorer
	Erstelle die benötigten Tabellen
	Erstelle Prozeduren für die Abfragen der Tabellen um Sicherheit zu gewährleisten





2. Wir machen ein Transfer-Daten-Objekt
	
	Eine Assembly mit dem Gebiet für die SqlController
	Eine Assembly für die Daten des Gebiets

3. Wir machen ein Model

	Fügen eine Aufgabe hinzu
	Füge eine View zu der Aufgabe hinzu
	Wir machen einen WebController, welcher den SqlController aufruft
