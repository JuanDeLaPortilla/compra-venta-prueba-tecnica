import { Button } from "@/shared/components/ui/button";
import { Card, CardContent } from "@/shared/components/ui/card";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/shared/components/ui/table";
import type { KardexProduct } from "../../domain/kardex";
import { Badge } from "@/shared/components/ui/badge";
import { ArrowLeftRight } from "lucide-react";
import { money } from "@/shared/lib/utils";

interface KardexTableProps {
  products: KardexProduct[];
  loading: boolean;
  onViewMovements: (product: KardexProduct) => void;
}

export const KardexTable = ({
  products,
  loading,
  onViewMovements,
}: KardexTableProps) => {
  return (
    <Card className="shadow-none">
      <CardContent className="p-0">
        <Table>

          <TableHeader>
            <TableRow>
              <TableHead className="pl-5">Producto</TableHead>
              <TableHead>Stock actual</TableHead>
              <TableHead>Costo</TableHead>
              <TableHead>Precio venta</TableHead>
              <TableHead className="pr-5 text-right">Acciones</TableHead>
            </TableRow>
          </TableHeader>

          <TableBody>
            {loading && (
              <TableRow>
                <TableCell
                  colSpan={5}
                  className="h-24 text-center"
                >
                  Cargando productos...
                </TableCell>
              </TableRow>
            )}

            {!loading && products.length === 0 && (
              <TableRow>
                <TableCell
                  colSpan={5}
                  className="h-24 text-center text-muted-foreground"
                >
                  No se encontraron productos.
                </TableCell>
              </TableRow>
            )}

            {!loading &&
              products.map((product) => (
                <TableRow key={product.productId}>
                  <TableCell className="pl-5 font-medium">
                    {product.productName}

                    <p className="text-xs font-normal text-muted-foreground">
                      ID: {product.productId}
                    </p>
                  </TableCell>

                  <TableCell>
                    <Badge
                      variant={
                        product.stock < 10
                          ? "destructive"
                          : "secondary"
                      }
                    >
                      {product.stock} und.
                    </Badge>
                  </TableCell>

                  <TableCell>
                    {money(product.cost)}
                  </TableCell>

                  <TableCell>
                    {money(product.salePrice)}
                  </TableCell>

                  <TableCell className="pr-5 text-right">
                    <Button
                      variant="outline"
                      size="sm"
                      onClick={() =>
                        onViewMovements(product)
                      }
                    >
                      <ArrowLeftRight /> Ver movimientos
                    </Button>
                  </TableCell>
                </TableRow>
              ))}
          </TableBody>

        </Table>
      </CardContent>
    </Card>
  );
}