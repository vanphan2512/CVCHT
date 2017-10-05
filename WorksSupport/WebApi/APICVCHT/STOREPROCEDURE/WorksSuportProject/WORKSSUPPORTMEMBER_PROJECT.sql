CREATE OR REPLACE PROCEDURE ERP.WORKSSUPPORTMEMBER_PROJECT
/*
	CREATED BY	:	Nguyen Van Huan
	DATE		:	27.06.2017
	DESCRIPTION	:	LAY TAT CA MEMBERS THEO DU AN
*/
(	 
  V_PROJECTID IN EO_WORKSSUPPORTPROJECT.WORKSSUPPORTPROJECTID%TYPE,
  V_OUT OUT SYS_REFCURSOR
)
AS
BEGIN
 OPEN V_OUT FOR
      SELECT *
      FROM eo_workssupportproject_member ew
      LEFT JOIN sys_user su
      ON ew.memberusername = su.username
      LEFT JOIN sys_department sd  
      ON su.departmentid = sd.departmentid 
      WHERE 
        su.isdeleted = 0 AND 
        sd.isdeleted =0  AND 
        ew.isdeleted =0 AND
        ew.workssupportprojectid =V_PROJECTID ;                                                                                                                                                                                                                                        
END;
/