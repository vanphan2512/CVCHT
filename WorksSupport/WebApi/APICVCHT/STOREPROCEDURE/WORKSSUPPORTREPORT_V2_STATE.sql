CREATE OR REPLACE PROCEDURE ERP.WORKSSUPPORTREPORT_V2_STATE
/*
	CREATED BY	:	NGUYEN VAN HUAN
	      DATE  :	11/09/2017
	DESCRIPTION	:	TH?NG KÊ CÔNG VI?C THEO TÌNH TR?NG 
*/
( 
   V_OUT OUT SYS_REFCURSOR,
   V_WORKSSUPPORTPROJECTID IN EO_WORKSSUPPORTGROUP.WORKSSUPPORTPROJECTID%TYPE,
   V_WORKSSUPPORTGROUPID IN EO_WORKSSUPPORTGROUP.WORKSSUPPORTGROUPID%TYPE,
   V_FROMDATE IN EO_WORKSSUPPORT.CREATEDDATE%TYPE,
   V_TODATE IN EO_WORKSSUPPORT.CREATEDDATE%TYPE
  )
AS 
BEGIN
  OPEN V_OUT FOR
     SELECT (SELECT COUNT(1) 
               FROM EO_WORKSSUPPORT E_WORK
              WHERE EXISTS (SELECT E_GROUP.WORKSSUPPORTGROUPID
                              FROM EO_WORKSSUPPORTGROUP E_GROUP
                             WHERE E_GROUP.WORKSSUPPORTGROUPID = E_WORK.WORKSSUPPORTGROUPID
                                   AND E_GROUP.WORKSSUPPORTPROJECTID = V_WORKSSUPPORTPROJECTID
                                   AND E_GROUP.ISDELETED =0
                                   AND (V_WORKSSUPPORTGROUPID IS NULL OR E_GROUP.WORKSSUPPORTGROUPID = V_WORKSSUPPORTGROUPID)
                                   AND EXISTS (SELECT E_PROJECT.WORKSSUPPORTPROJECTID
                                                 FROM EO_WORKSSUPPORTPROJECT E_PROJECT
                                                WHERE E_PROJECT.ISACTIVE = 1
                                                      AND E_PROJECT.ISDELETED =0
                                                      AND E_PROJECT.WORKSSUPPORTPROJECTID = V_WORKSSUPPORTPROJECTID
                                                      AND E_PROJECT.WORKSSUPPORTPROJECTID = E_GROUP.WORKSSUPPORTPROJECTID
                                                )
                              )
                    AND TRUNC(E_WORK.COMPLETEDDATE) < TRUNC(E_WORK.EXPECTEDCOMPLETEDDATE)
                    AND E_WORK.ISDELETED = 0
                    AND (V_FROMDATE IS NULL OR TRUNC(V_FROMDATE) <= TRUNC(E_WORK.CREATEDDATE))
                    AND (V_TODATE IS NULL OR TRUNC(V_TODATE) >= TRUNC(E_WORK.CREATEDDATE))
              ) AS NUMBEROFWORKLATE
             ,(SELECT count(1) 
                 FROM (SELECT E_WORK.WORKSSUPPORTID
                 FROM EO_WORKSSUPPORT E_WORK
            LEFT JOIN EO_WORKSSUPPORT_WORKFLOW E_WORK_FLOW
                   ON E_WORK.WORKSSUPPORTID = E_WORK_FLOW.WORKSSUPPORTID
                      AND E_WORK_FLOW.ISDELETED = 0
            LEFT JOIN EO_WORKSSUPPORTTYPE_WORKFLOW E_TYPE_FLOW
                   ON E_WORK_FLOW.WORKSSUPPORTID = E_WORK.WORKSSUPPORTID
                      AND E_WORK_FLOW.WORKSSUPPORTSTEPID = E_TYPE_FLOW.WORKSSUPPORTSTEPID  
                      AND E_TYPE_FLOW.ISDELETED =0                       
                WHERE EXISTS (SELECT E_GROUP.WORKSSUPPORTGROUPID
                                FROM EO_WORKSSUPPORTGROUP E_GROUP
                               WHERE E_GROUP.WORKSSUPPORTGROUPID = E_WORK.WORKSSUPPORTGROUPID
                                     AND E_GROUP.WORKSSUPPORTPROJECTID = V_WORKSSUPPORTPROJECTID
                                     AND E_GROUP.ISDELETED =0
                                     AND (V_WORKSSUPPORTGROUPID IS NULL OR E_GROUP.WORKSSUPPORTGROUPID = V_WORKSSUPPORTGROUPID)
                                     AND EXISTS (SELECT E_PROJECT.WORKSSUPPORTPROJECTID
                                                   FROM EO_WORKSSUPPORTPROJECT E_PROJECT
                                                  WHERE E_PROJECT.ISACTIVE = 1
                                                        AND E_PROJECT.ISDELETED =0
                                                        AND E_PROJECT.WORKSSUPPORTPROJECTID = V_WORKSSUPPORTPROJECTID
                                                        AND E_PROJECT.WORKSSUPPORTPROJECTID = E_GROUP.WORKSSUPPORTPROJECTID
                                                  )
                              )
                      AND E_WORK.ISDELETED = 0
                      AND (V_FROMDATE IS NULL OR TRUNC(V_FROMDATE) <= TRUNC(E_WORK.CREATEDDATE))
                      AND (V_TODATE IS NULL OR TRUNC(V_TODATE) >= TRUNC(E_WORK.CREATEDDATE))                      
                      AND (24*( EXTRACT(DAY FROM (E_WORK_FLOW.WORKSSUPPORTDATE-E_WORK_FLOW.PROCESSDATE ))) + EXTRACT(HOUR FROM (E_WORK_FLOW.WORKSSUPPORTDATE-E_WORK_FLOW.PROCESSDATE)) > E_TYPE_FLOW.MAXPROCESSTIME                                             
                           OR (E_WORK_FLOW.PROCESSDATE IS NULL 
                               AND 24*( EXTRACT(DAY FROM (sysdate-E_WORK_FLOW.PROCESSDATE ))) + EXTRACT(HOUR FROM (sysdate-E_WORK_FLOW.PROCESSDATE)) > E_TYPE_FLOW.MAXPROCESSTIME                                             
                              )
                           )
                 GROUP BY E_WORK.WORKSSUPPORTID
               ) wrong)        
        AS NUMBEROFWORKWRONG
       FROM DUAL;
END;
/