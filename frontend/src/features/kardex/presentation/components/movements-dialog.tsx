import { Badge } from "@/shared/components/ui/badge";

import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
} from "@/shared/components/ui/dialog";

import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/shared/components/ui/table";
import type { KardexProduct } from "../../domain/kardex";
import { useProductMovements } from "../../hooks/use-product-movements";


export interface MovementsDialogProps {
  open: boolean;
  product: KardexProduct | null;
  onOpenChange: (open: boolean) => void;
}

const formatDate = (date: string) => {
  return new Intl.DateTimeFormat("es-PE", {
    dateStyle: "medium",
    timeStyle: "short",
  }).format(new Date(date));
};

export const MovementsDialog = ({
  open,
  product,
  onOpenChange,
}: MovementsDialogProps) => {
  const {
    data: movements = [],
    isLoading,
    isError,
  } = useProductMovements(
    product?.productId ?? null
  );

  return (
    <Dialog
      open={open}
      onOpenChange={onOpenChange}
    >
      <DialogContent className="max-w-3xl">
        <DialogHeader>
          <DialogTitle>
            Movimientos de {product?.productName}
          </DialogTitle>

          <DialogDescription>
            Historial de entradas y salidas del inventario.
          </DialogDescription>
        </DialogHeader>

        <div className="mt-4">
          {isLoading && (
            <div className="flex h-32 items-center justify-center text-sm text-muted-foreground">
              Cargando movimientos...
            </div>
          )}

          {isError && (
            <div className="rounded-lg border border-destructive/30 bg-destructive/10 p-4 text-sm text-destructive">
              No se pudieron cargar los movimientos del
              producto.
            </div>
          )}

          {!isLoading &&
            !isError &&
            movements.length === 0 && (
              <div className="flex h-32 items-center justify-center text-sm text-muted-foreground">
                Este producto no tiene movimientos registrados.
              </div>
            )}

          {!isLoading &&
            !isError &&
            movements.length > 0 && (
              <div className="overflow-hidden rounded-lg border">
                <Table>
                  <TableHeader>
                    <TableRow>
                      <TableHead>
                        Fecha
                      </TableHead>

                      <TableHead>
                        Tipo
                      </TableHead>

                      <TableHead>
                        Cantidad
                      </TableHead>
                    </TableRow>
                  </TableHeader>

                  <TableBody>
                    {movements.map(
                      (movement, index) => (
                        <TableRow
                          key={`${movement.registrationDate}-${index}`}
                        >
                          <TableCell>
                            {formatDate(
                              movement.registrationDate
                            )}
                          </TableCell>

                          <TableCell>
                            <Badge
                              variant={
                                movement.movementType ===
                                  "Entrada"
                                  ? "secondary"
                                  : "outline"
                              }
                              className={
                                movement.movementType ===
                                  "Entrada"
                                  ? "text-emerald-700"
                                  : "text-amber-700"
                              }
                            >
                              {movement.movementType}
                            </Badge>
                          </TableCell>

                          <TableCell className="font-medium">
                            <span
                              className={
                                movement.movementType ===
                                  "Entrada"
                                  ? "text-emerald-600"
                                  : "text-amber-600"
                              }
                            >
                              {movement.movementType ===
                                "Entrada"
                                ? "+"
                                : "-"}
                              {movement.quantity}
                            </span>
                          </TableCell>
                        </TableRow>
                      )
                    )}
                  </TableBody>
                </Table>
              </div>
            )}
        </div>
      </DialogContent>
    </Dialog>
  );
};