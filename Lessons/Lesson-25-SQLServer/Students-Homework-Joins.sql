--SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
--BEGIN TRAN
select * from Students WITH (NOLOCK)
left join HomeWorks on HomeWorks.UserId = Students.Id
-- left hash join HomeWorks on HomeWorks.UserId = Students.Id
-- left loop join HomeWorks on HomeWorks.UserId = Students.Id
-- left merge join HomeWorks on HomeWorks.UserId = Students.Id
order by Students.[Name] DESC