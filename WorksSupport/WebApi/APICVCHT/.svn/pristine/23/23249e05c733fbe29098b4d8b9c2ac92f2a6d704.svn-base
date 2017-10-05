CREATE OR REPLACE PROCEDURE ERP.EO_WS_PRIORITY_CHK_USED
/*
	CREATED BY	:	NGUYEN VAN HUAN 
	DATE		    :	04/10/2017
	DESCRIPTION	:	KIEM TRA DO UU TIEN DA DUOC SU DUNG 
*/
(   
    V_WORKSSUPPORTPRIORITYID EO_WORKSSUPPORTPRIORITY.WORKSSUPPORTPRIORITYID%TYPE,
    V_OUT OUT NUMBER    
) 
AS
   V_IS_USED NUMBER := 0;
BEGIN
   BEGIN
	 SELECT COUNT(1) INTO V_IS_USED
  	 FROM EO_WORKSSUPPORTPRIORITY  E_PRIORITY
     WHERE E_PRIORITY.WORKSSUPPORTPRIORITYID = V_WORKSSUPPORTPRIORITYID
           AND E_PRIORITY.ISDELETED =0
           AND  EXISTS (SELECT E_WORK.workssupportid
                          FROM EO_WORKSSUPPORT E_WORK
                         WHERE E_WORK.WORKSSUPPORTPRIORITYID = E_PRIORITY.WORKSSUPPORTPRIORITYID
                               AND e_work.WORKSSUPPORTPRIORITYID = V_WORKSSUPPORTPRIORITYID
                               AND E_WORK.ISDELETED = 0
                               AND EXISTS (SELECT E_GROUP.workssupportgroupid
                                              FROM EO_WORKSSUPPORTGROUP E_GROUP
                                             WHERE E_GROUP.WORKSSUPPORTGROUPID = E_WORK.WORKSSUPPORTGROUPID
                                                   AND E_GROUP.ISDELETED =0
                                                   AND EXISTS (SELECT e_project.workssupportprojectid
                                                                 FROM EO_WORKSSUPPORTPROJECT E_PROJECT
                                                                WHERE E_PROJECT.WORKSSUPPORTPROJECTID = E_GROUP.WORKSSUPPORTPROJECTID
                                                                      AND E_PROJECT.ISDELETED =0
                                                                      AND EXISTS (SELECT E_TYPE.workssupporttypeid
                                                                                    FROM EO_WORKSSUPPORTTYPE E_TYPE
                                                                                   WHERE E_PROJECT.WORKSSUPPORTTYPEID = E_TYPE.WORKSSUPPORTTYPEID
                                                                                         AND E_TYPE.ISDELETED =0
                                                                                  )
                                                                )
                                              )
                        );
    EXCEPTION WHEN OTHERS THEN V_IS_USED := 0;
    END;
    V_OUT := V_IS_USED;
END;
/