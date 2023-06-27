select A.Country, sum(A.GoldMedal) as [Gold Medals], sum(A.SilverMedal) as [Silver Medals], sum(A.BronzeMedal) as [Bronze Medals]
from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
and OI.Id = 2
group by  A.Country


select A.Name + ' ' + A.Surname as [Full Name], KS.Name as [Kind of Sport], sum(A.GoldMedal) as [Gold Medals], sum(A.SilverMedal) as [Silver Medals], sum(A.BronzeMedal) as [Bronze Medals]
from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
group by A.Name + ' ' + A.Surname, KS.Name
order by KS.Name


select top(1) A.Country, max(A.GoldMedal) as [Most Gold Medals]
from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
group by A.Country
order by max(A.GoldMedal) desc


select top(1) A.Name + ' ' + A.Surname as [Full Name], sum(A.GoldMedal) as [Most Gold Medals]
from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
and KS.Name = 'Kind2'
group by A.Name + ' ' + A.Surname
order by sum(A.GoldMedal) desc


select top(1) HostCountryName as [Host Country], count(HostCountryName) as Amount
from OlympicsInfo 
group by HostCountryName
order by count(HostCountryName) desc



select Name + ' ' + Surname as [Full Name]
from Athletes
where Country = 'Mulehe'


select A.Country, sum(A.GoldMedal) as [Gold Medals], sum(A.SilverMedal) as [Silver Medals], sum(A.BronzeMedal) as [Bronze Medals], sum(KSA.Result) as Result
from Athletes as A, KindOfSports as KS, OlympicsInfo as OI, KindOfSportsAndAthletes as KSA, OlympicsOverallInfo as OOI
where A.Id = KSA.AthleteId and KS.Id = KSA.KindOfSportId and OI.Id = OOI.OlympicsInfoId and KSA.Id = OOI.KindOfSportAndAthleteId
and OI.Id = 2
group by A.Country
order by sum(KSA.Result) desc