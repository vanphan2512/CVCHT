CREATE OR REPLACE PROCEDURE ERP.WS_REPORT_V2_WDetail_STATE
/*
	CREATED BY	:	NGUYEN VAN HUAN
	      DATE  :	11/09/2017
	DESCRIPTION	:	TH?NG K� C�NG VI?C THEO T�NH TR?NG 
*/
( 
   V_OUT OUT SYS_REFCURSOR,
   V_WORKSSUPPORTGROUPID IN EO_WORKSSUPPORTGROUP.WORKSSUPPORTGROUPID%TYPE,
   V_FROMDATE IN EO_WORKSSUPPORT.CREATEDDATE%TYPE,
   V_TODATE IN EO_WORKSSUPPORT.CREATEDDATE%TYPE,
   V_TYPE IN NUMBER
  )
AS 
BEGIN
  IF (V_TYPE = 1) THEN     
   OPEN V_OUT FOR
      SELECT E_WORK.WORKSSUPPORTID
            ,E_WORK.WORKSSUPPORTNAME 
            ,E_WORK.EXPECTEDCOMPLETEDDATE
            ,E_WORK.COMPLETEDDATE
            ,EXTRACT(DAY FROM (E_WORK.COMPLETEDDATE-E_WORK.EXPECTEDCOMPLETEDDATE ) ) AS LATEDATE
            ,  (SELECT COUNT(1)
                  FROM EO_WORKSSUPPORT_WORKFLOW E_WORK_FLOW
             LEFT JOIN EO_WORKSSUPPORTTYPE_WORKFLOW E_TYPE_FLOW
                    ON E_WORK_FLOW.WORKSSUPPORTSTEPID = E_TYPE_FLOW.WORKSSUPPORTSTEPID
                 WHERE E_WORK.WORKSSUPPORTID = E_WORK_FLOW.WORKSSUPPORTID
                         AND (24*( EXTRACT(DAY FROM (E_WORK_FLOW.WORKSSUPPORTDATE-E_WORK_FLOW.PROCESSDATE ))) + EXTRACT(HOUR FROM (E_WORK_FLOW.WORKSSUPPORTDATE-E_WORK_FLOW.PROCESSDATE)) > E_TYPE_FLOW.MAXPROCESSTIME                                             
                               OR (    E_WORK_FLOW.PROCESSDATE IS NULL 
                                   AND 24*( EXTRACT(DAY FROM (SYSDATE-E_WORK_FLOW.WORKSSUPPORTDATE ))) + EXTRACT(HOUR FROM (SYSDATE-E_WORK_FLOW.WORKSSUPPORTDATE)) > E_TYPE_FLOW.MAXPROCESSTIME                                             
                                  )
                               )
                 ) AS NUMBEROFWORK
             ,E_WORK.CURRENTPROGRESS
             ,(SELECT W_STATUS.WORKSSUPPORTSTATUSNAME
                 FROM EO_WORKSSUPPORTSTATUS W_STATUS
                WHERE W_STATUS.WORKSSUPPORTSTATUSID = E_WORK.WORKSSUPPORTSTATUSID
                      AND W_STATUS.ISDELETED = 0
                ) AS WORKSSUPPORTSTATUSNAME
        FROM  eo_workssupport e_work
       WHERE  E_WORK.ISDELETED = 0
              AND E_WORK.WORKSSUPPORTGROUPID = V_WORKSSUPPORTGROUPID
              AND E_GROUP.WORKSSUPPORTPROJECTID = V_WORKSSUPPORTPROJECTID
              AND E_GROUP.ISDELETED =0
              AND (V_WORKSSUPPORTGROUPID IS NULL OR E_GROUP.WORKSSUPPORTGROUPID = V_WORKSSUPPORTGROUPID)
              AND TRUNC(E_WORK.COMPLETEDDATE) < TRUNC(E_WORK.EXPECTEDCOMPLETEDDATE)
              AND (V_FROMDATE IS NULL OR TRUNC(V_FROMDATE) <= TRUNC(E_WORK.CREATEDDATE))
              AND (V_TODATE IS NULL OR TRUNC(V_TODATE) >= TRUNC(E_WORK.CREATEDDATE)) 
      GROUP BY E_GROUP.WORKSSUPPORTGROUPID, E_GROUP.WORKSSUPPORTGROUPNAME;  
 ELSIF V_TYPE =2 THEN
    OPEN V_OUT FOR
      SELECT  W_WRONG.WORKSSUPPORTGROUPID
             ,W_WRONG.WORKSSUPPORTGROUPNAME
             ,COUNT(1) AS NUMBEROFWORK
        FROM (
      SELECT  E_GROUP.WORKSSUPPORTGROUPID
              ,E_GROUP.WORKSSUPPORTGROUPNAME
              ,E_WORK.WORKSSUPPORTID
        FROM  EO_WORKSSUPPORTGROUP E_GROUP
   LEFT JOIN  EO_WORKSSUPPORT E_WORK
          ON  E_GROUP.WORKSSUPPORTGROUPID = E_WORK.WORKSSUPPORTGROUPID
              AND E_WORK.ISDELETED = 0
    LEFT JOIN EO_WORKSSUPPORT_WORKFLOW E_WORK_FLOW
           ON E_WORK.WORKSSUPPORTID = E_WORK_FLOW.WORKSSUPPORTID
              AND E_WORK_FLOW.ISDELETED = 0
    LEFT JOIN EO_WORKSSUPPORTTYPE_WORKFLOW E_TYPE_FLOW
           ON E_WORK_FLOW.WORKSSUPPORTID = E_WORK.WORKSSUPPORTID
              AND E_WORK_FLOW.WORKSSUPPORTSTEPID = E_TYPE_FLOW.WORKSSUPPORTSTEPID  
              AND E_TYPE_FLOW.ISDELETED =0  
              AND (V_WORKSSUPPORTGROUPID IS NULL OR E_WORK.WORKSSUPPORTGROUPID = V_WORKSSUPPORTGROUPID)              
       WHERE  E_GROUP.WORKSSUPPORTGROUPID = E_WORK.WORKSSUPPORTGROUPID
              AND E_GROUP.WORKSSUPPORTPROJECTID = V_WORKSSUPPORTPROJECTID
              AND E_GROUP.ISDELETED =0
              AND (V_WORKSSUPPORTGROUPID IS NULL OR E_GROUP.WORKSSUPPORTGROUPID = V_WORKSSUPPORTGROUPID)
              AND (V_FROMDATE IS NULL OR TRUNC(V_FROMDATE) <= TRUNC(E_WORK.CREATEDDATE))
              AND (V_TODATE IS NULL OR TRUNC(V_TODATE) >= TRUNC(E_WORK.CREATEDDATE))                      
              AND (24*( EXTRACT(DAY FROM (E_WORK_FLOW.WORKSSUPPORTDATE-E_WORK_FLOW.PROCESSDATE ))) + EXTRACT(HOUR FROM (E_WORK_FLOW.WORKSSUPPORTDATE-E_WORK_FLOW.PROCESSDATE)) > E_TYPE_FLOW.MAXPROCESSTIME                                             
                   OR (    E_WORK_FLOW.PROCESSDATE IS NULL 
                       AND 24*( EXTRACT(DAY FROM (SYSDATE-E_WORK_FLOW.WORKSSUPPORTDATE ))) + EXTRACT(HOUR FROM (SYSDATE-E_WORK_FLOW.WORKSSUPPORTDATE)) > E_TYPE_FLOW.MAXPROCESSTIME                                             
                      )
                   )
    GROUP BY E_GROUP.WORKSSUPPORTGROUPID, E_GROUP.WORKSSUPPORTGROUPNAME, E_WORK.WORKSSUPPORTID
        ) W_WRONG
    GROUP BY W_WRONG.WORKSSUPPORTGROUPID, W_WRONG.WORKSSUPPORTGROUPNAME ;
  END IF; 
END;
/