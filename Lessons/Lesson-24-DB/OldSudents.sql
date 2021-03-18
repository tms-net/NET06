select * from Students
where (YEAR(GETDATE()) - YEAR([BirthDay])) > 30