
declare @DateStart Date
set @DateStart = '20150101'
declare @DateEnd Date
set @DateEnd = '20151231'
SELECT     derivedtbl_1.F2 AS ������������������������, derivedtbl_1.Expr1 AS �����, derivedtbl_2.Expr1 AS �����������������, 
                      derivedtbl_3.Expr1 AS �������������������������, derivedtbl_4.Expr1 AS �����������������������������������, 
                      derivedtbl_5.Expr1 AS ������������������, derivedtbl_6.Expr1 AS ����������������������
FROM         (SELECT     F2, SUM(�����) AS Expr1
                       FROM          dbo.View���������������
                       WHERE      (������������������ >= @DateStart) AND (������������������ <= @DateEnd)
                       GROUP BY F2) AS derivedtbl_1 LEFT OUTER JOIN
                          (SELECT     �����������������, SUM(�����) AS Expr1, F2
                            FROM          dbo.View��������������� AS View���������������_5
                            WHERE      (����������������� = '����������������� ����') AND (������������������ >= @DateStart) AND 
                                                   (������������������ <= @DateEnd)
                            GROUP BY �����������������, F2) AS derivedtbl_6 ON derivedtbl_1.F2 = derivedtbl_6.F2 LEFT OUTER JOIN
                          (SELECT     �����������������, SUM(�����) AS Expr1, F2
                            FROM          dbo.View��������������� AS View���������������_4
                            WHERE      (����������������� = '�������� ����') AND (������������������ >= @DateStart) AND (������������������ <= @DateEnd)
                            GROUP BY �����������������, F2) AS derivedtbl_5 ON derivedtbl_1.F2 = derivedtbl_5.F2 LEFT OUTER JOIN
                          (SELECT     �����������������, SUM(�����) AS Expr1, F2
                            FROM          dbo.View��������������� AS View���������������_3
                            WHERE      (����������������� = '������� ����� ����������� �������') AND (������������������ >= @DateStart) AND 
                                                   (������������������ <= @DateEnd)
                            GROUP BY �����������������, F2) AS derivedtbl_4 ON derivedtbl_1.F2 = derivedtbl_4.F2 LEFT OUTER JOIN
                          (SELECT     �����������������, SUM(�����) AS Expr1, F2
                            FROM          dbo.View��������������� AS View���������������_2
                            WHERE      (����������������� = '�������  ������� ������') AND (������������������ >= @DateStart) AND 
                                                   (������������������ <= @DateEnd)
                            GROUP BY �����������������, F2) AS derivedtbl_3 ON derivedtbl_1.F2 = derivedtbl_3.F2 LEFT OUTER JOIN
                          (SELECT     �����������������, SUM(�����) AS Expr1, F2
                            FROM          dbo.View��������������� AS View���������������_1
                            WHERE      (����������������� = '������� �����') AND (������������������ >= @DateStart) AND (������������������ <= @DateEnd)
                            GROUP BY �����������������, F2) AS derivedtbl_2 ON derivedtbl_1.F2 = derivedtbl_2.F2