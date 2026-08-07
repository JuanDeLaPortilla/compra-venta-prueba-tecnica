import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow
} from "@/shared/components/ui/table";

import { money } from "@/shared/lib/utils";
import type { Purchase } from "../../domain/purchase";
import { Card, CardContent } from "@/shared/components/ui/card";

interface Props {
  purchases: Purchase[];
  loading: boolean;
}

export const PurchasesTable = ({
  purchases,
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
                  Cargando compras...
                </TableCell>
              </TableRow>
            )}

            {!loading && purchases.length === 0 && (
              <TableRow>
                <TableCell
                  colSpan={5}
                  className="h-24 text-center"
                >
                  No se encontraron compras.
                </TableCell>
              </TableRow>
            )}

            {!loading && purchases.map((purchase) => (
              <TableRow
                key={purchase.id}
              >
                <TableCell className="pl-5 font-medium">
                  Compra #{purchase.id}
                </TableCell>

                <TableCell>
                  {new Date(
                    purchase.registrationDate
                  ).toLocaleDateString()}
                </TableCell>

                <TableCell>
                  {money(purchase.subTotal)}
                </TableCell>

                <TableCell>
                  {money(purchase.taxAmount)}
                </TableCell>

                <TableCell className="font-semibold">
                  {money(purchase.totalAmount)}
                </TableCell>
              </TableRow>
            ))}
          </TableBody>

        </Table>
      </CardContent>
    </Card>
  );
}