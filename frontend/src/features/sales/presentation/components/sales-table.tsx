import { Card, CardContent } from "@/shared/components/ui/card"
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/shared/components/ui/table"
import { money } from "@/shared/lib/utils";
import type { Sale } from "../../domain/sale";

interface Props {
  sales: Sale[];
  loading: boolean;
}

export const SalesTable = ({
  sales,
  loading
}: Props) => {
  return (
    <Card className="shadow-none">
      <CardContent className="p-0">
        <Table>

          <TableHeader>
            <TableRow>
              <TableHead className="pl-5">Código</TableHead>
              <TableHead>Fecha Registro</TableHead>
              <TableHead>Sub Total</TableHead>
              <TableHead>IGV</TableHead>
              <TableHead>Total</TableHead>
            </TableRow>
          </TableHeader>

          <TableBody>
            {loading && (
              <TableRow>
                <TableCell
                  colSpan={5}
                  className="h-24 text-center"
                >
                  Cargando ventas...
                </TableCell>
              </TableRow>
            )}

            {!loading && sales.length === 0 && (
              <TableRow>
                <TableCell
                  colSpan={5}
                  className="h-24 text-center"
                >
                  No se encontraron ventas.
                </TableCell>
              </TableRow>
            )}

            {!loading && sales.map((sale) => (
              <TableRow
                key={sale.id}
              >
                <TableCell className="pl-5 font-medium">
                  Venta #{sale.id}
                </TableCell>

                <TableCell>
                  {new Date(
                    sale.registrationDate
                  ).toLocaleDateString()}
                </TableCell>

                <TableCell>
                  {money(sale.subTotal)}
                </TableCell>

                <TableCell>
                  {money(sale.taxAmount)}
                </TableCell>

                <TableCell className="font-semibold">
                  {money(sale.totalAmount)}
                </TableCell>
              </TableRow>
            ))}
          </TableBody>


        </Table>
      </CardContent>
    </Card>
  );
}