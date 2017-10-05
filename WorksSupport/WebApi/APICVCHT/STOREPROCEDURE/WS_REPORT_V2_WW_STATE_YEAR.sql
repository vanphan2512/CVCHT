CREATE OR REPLACE PROCEDURE ERP.WS_REPORT_V2_WW_STATE_YEAR
/*
	CREATED BY	:	NGUYEN VAN HUAN
	      DATE  :	11/09/2017
	DESCRIPTION	:	TH?NG K� C�NG VI?C THEO T�NH TR?NG(CONG VIEC VI PHAM THEO NAM)
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
        SELECT TO_CHAR(WORK_WRONG.DATETIME,'YYYY') AS TIMEWRONG
               ,COUNT(1)AS NUMBEROFWORKWRONG
         FROM (SELECT   
                        E_WORK.CREATEDDATE AS DATETIME
                        ,ROW_NUMBER() OVER (PARTITION BY E_WORK.WORKSSUPPORTID ORDER BY E_WORK.WORKSSUPPORTID) AS NUM 
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
              AND (V_FROMDATE IS NULL OR TO_CHAR(V_FROMDATE,'YYYY') <= TO_CHAR(E_WORK.CREATEDDATE, 'YYYY'))
              AND (V_TODATE IS NULL OR TO_CHAR(V_TODATE,'YYYY') >= TO_CHAR(E_WORK.CREATEDDATE, 'YYYY'))                       
              AND (24*( EXTRACT(DAY FROM (E_WORK_FLOW.WORKSSUPPORTDATE-E_WORK_FLOW.PROCESSDATE ))) + EXTRACT(HOUR FROM (E_WORK_FLOW.WORKSSUPPORTDATE-E_WORK_FLOW.PROCESSDATE)) <= E_TYPE_FLOW.MAXPROCESSTIME                                             
                   OR (E_WORK_FLOW.PROCESSDATE IS NULL 
                       AND 24*( EXTRACT(DAY FROM (SYSDATE-E_WORK_FLOW.PROCESSDATE ))) + EXTRACT(HOUR FROM (SYSDATE-E_WORK_FLOW.PROCESSDATE)) > E_TYPE_FLOW.MAXPROCESSTIME                                             
                      )
                   )
         
       ) WORK_WRONG
   WHERE WORK_WRONG.NUM = 1
   GROUP BY TO_CHAR(WORK_WRONG.DATETIME,'YYYY');
END;
/