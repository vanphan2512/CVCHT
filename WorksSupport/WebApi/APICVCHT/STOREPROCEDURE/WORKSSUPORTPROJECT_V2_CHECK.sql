CREATE OR REPLACE PROCEDURE ERP.WORKSSUPORTPROJECT_V2_CHECK
/*	CREATED BY	:	NGUYEN VAN HUAN
	       DATE 	:	21.6.2017
   	DESCRIPTION	:	KIEM TRA TEN TRUNG NHAU TRONG BANG EO_WORKSSUPPORTPROJECT
*/
(	
  V_WORKSSUPPORTPROJECTID IN EO_WORKSSUPPORTPROJECT.WORKSSUPPORTPROJECTID%TYPE,
	V_WORKSSUPPORTPROJECTNAME IN EO_WORKSSUPPORTPROJECT.WORKSSUPPORTPROJECTNAME%TYPE,
  V_ORDERINDEX IN EO_WORKSSUPPORTPROJECT.ORDERINDEX%TYPE,
	V_OUT OUT SYS_REFCURSOR
) 
AS
 T_CHECK_NAME NUMBER := 0;
 T_CHECK_ORDER NUMBER := 0;
BEGIN
  -- CHECK WORKSSUPPORTSTATUSNAME EXISTED
  BEGIN
    SELECT COUNT(1) INTO T_CHECK_NAME
      FROM EO_WORKSSUPPORTPROJECT EO
     WHERE     TRIM(UPPER(EO.WORKSSUPPORTPROJECTNAME)) = TRIM(UPPER(V_WORKSSUPPORTPROJECTNAME)) 
           AND EO.WORKSSUPPORTPROJECTID <> V_WORKSSUPPORTPROJECTID  
           AND EO.ISDELETED =0;    
    EXCEPTION WHEN OTHERS THEN T_CHECK_NAME := 0;
  END;
  
  -- CHECK ORDERINDEX EXISTED
  BEGIN
    SELECT COUNT(1) INTO T_CHECK_ORDER
      FROM EO_WORKSSUPPORTPROJECT EO
     WHERE     EO.ORDERINDEX = V_ORDERINDEX 
           AND EO.WORKSSUPPORTPROJECTID <> V_WORKSSUPPORTPROJECTID
           AND EO.ISDELETED =0;    
    EXCEPTION WHEN OTHERS THEN T_CHECK_ORDER := 0;
  END;
      
  -- RETURN TABLE
  OPEN V_OUT FOR
    SELECT T_CHECK_NAME AS PROJECTNAME, T_CHECK_ORDER AS ORDERINDEX
      FROM DUAL D;
  RETURN;    

END;
/