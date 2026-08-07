import { Separator } from "@/shared/components/ui/separator";
import { money } from "@/shared/lib/utils";

interface PurchaseSummaryProps {
  productsCount: number;
  units: number;
  subtotal: number;
  taxAmount: number;
  total: number;
}

export function PurchaseSummary({
  productsCount, units, subtotal, taxAmount, total
}: PurchaseSummaryProps) {
  return (
    <div className="ml-auto w-full max-w-sm space-y-2">
      <div className="flex items-center justify-between text-sm">
        <span className="text-muted-foreground">Productos</span>
        <span className="font-medium tabular-nums">{productsCount}</span>
      </div>

      <div className="flex items-center justify-between text-sm">
        <span className="text-muted-foreground">Unidades</span>
        <span className="font-medium tabular-nums">{units}</span>
      </div>

      <Separator />

      <div className="grid gap-2">
        <div className="flex items-center justify-between text-sm">
          <span className="text-muted-foreground">
            Subtotal
          </span>

          <span className="font-medium tabular-nums">
            {money(subtotal)}
          </span>
        </div>

        <div className="flex items-center justify-between text-sm">
          <span className="text-muted-foreground">
            IGV (18%)
          </span>

          <span className="font-medium tabular-nums">
            {money(taxAmount)}
          </span>
        </div>

        <Separator />

        <div className="flex items-center justify-between rounded-md">
          <span className="text-sm font-medium">
            Total de compra
          </span>

          <span className="text-lg font-semibold tabular-nums">
            {money(total)}
          </span>
        </div>
      </div>
    </div>
  );
}
