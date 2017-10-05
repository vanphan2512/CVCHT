CREATE OR REPLACE PROCEDURE ERP.WORKSDEPARTMENT_V2_GETALL
  /*
	CREATED BY	:	NGUY?N VAN PH?N
	DATE		:	29/06/2017
  DESCRIPTION	:	N?p t?t c? danh s�ch ph�ng ban
*/
(
  V_OUT OUT SYS_REFCURSOR
)
AS
BEGIN
  OPEN V_OUT FOR
  SELECT
    SYS_DEPARTMENT.DEPARTMENTID,
    SYS_DEPARTMENT.DEPARTMENTNAME
  FROM
    ERP.SYS_DEPARTMENT
  WHERE
     SYS_DEPARTMENT.ISDELETED = 0
    AND SYS_DEPARTMENT.ISACTIVE = 1
  ORDER BY
    SYS_DEPARTMENT.ORDERINDEX
  ;
END;
/