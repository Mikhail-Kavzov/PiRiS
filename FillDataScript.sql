INSERT INTO public."Disabilities"(
	"DisabilityId", "DisabilityStatus")
	VALUES 
	(1, 'None'),
	(2, '1 group'),
	(3, '2 group'),
	(4, '3 group');

INSERT INTO public."Citizenships"(
	"CitizenshipId", "CitizenshipName")
	VALUES 
	(1, 'Belarus'),
	(2, 'Russia'),
	(3, 'Ukraine'),
	(4, 'Poland'),
	(5, 'Germany'),
	(6, 'Lithuania'),
	(7, 'France'),
	(8, 'United Kingdom'),
	(9, 'Italy'),
	(10, 'USA');

INSERT INTO public."Cities"(
	"CityId", "Name")
	VALUES 
	(1, 'Minsk'),
	(2, 'Vitebsk'),
	(3, 'Grodno'),
	(4, 'Brest'),
	(5, 'Mogilev'),
	(6, 'Gomel'),
	(7, 'Warsaw'),
	(8, 'London'),
	(9, 'Berlin'),
	(10, 'Paris');

INSERT INTO public."FamilyStatuses"(
	"FamilyStatusId", "StatusName")
	VALUES
	(1, 'Not married'),
	(2, 'Married'),
	(3, 'Engaged'),
	(4, 'Divorced');