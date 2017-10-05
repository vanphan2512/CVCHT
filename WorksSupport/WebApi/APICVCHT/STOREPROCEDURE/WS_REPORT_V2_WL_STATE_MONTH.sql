CREATE OR REPLACE PROCEDURE ERP.WS_REPORT_V2_WL_STATE_MONTH
/*
	CREATED BY	:	NGUYEN VAN HUAN
	      DATE  :	11/09/2017
	DESCRIPTION	:	TH?NG K� C�NG VI?C THEO T�NH TR?NG CONG VIEC BI TRE THEO THANG 
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
     SELECT  TO_CHAR(E_WORK.CREATEDDATE,'MM/YYYY') AS TIMELATE
            ,COUNT(1) AS NUMBEROFWORKLATE
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
            AND (V_FROMDATE IS NULL OR TO_CHAR(V_FROMDATE,'YYYYMM') <= TO_CHAR(E_WORK.CREATEDDATE, 'YYYYMM'))
            AND (V_TODATE IS NULL OR TO_CHAR(V_TODATE,'YYYYMM') >= TO_CHAR(E_WORK.CREATEDDATE, 'YYYYMM'))                      
  GROUP BY TO_CHAR(E_WORK.CREATEDDATE,'MM/YYYY');       
END;
/