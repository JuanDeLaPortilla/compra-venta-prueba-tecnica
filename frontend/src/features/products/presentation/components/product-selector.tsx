import { useState } from "react";
import { Check, ChevronsUpDown, Plus, Search } from "lucide-react";
import { Button } from "@/shared/components/ui/button";
import {
  Command,
  CommandEmpty,
  CommandGroup,
  CommandInput,
  CommandItem,
  CommandList,
} from "@/shared/components/ui/command";
import { Popover, PopoverContent, PopoverTrigger } from "@/shared/components/ui/popover";
import { cn } from "@/shared/lib/utils";
import type { Product } from "../../domain/product";

interface ProductSelectorProps {
  products: Product[];
  selectedId: number | null;

  isLoading?: boolean;
  isError?: boolean;

  onSelect: (product: Product) => void;
  onRequestCreate?: () => void;
}

export function ProductSelector({
  products,
  selectedId,
  isLoading = false,
  isError = false,
  onSelect,
  onRequestCreate,
}: ProductSelectorProps) {
  const [open, setOpen] = useState(false);

  const selectedProduct = products.find(
    product => product.id === selectedId
  );

  return (
    <Popover open={open} onOpenChange={setOpen}>
      <PopoverTrigger
        render={
          <Button
            variant="outline"
            role="combobox"
            aria-expanded={open}
            className={cn(
              "w-full min-w-[200px] justify-between font-normal",
              !selectedId && "text-muted-foreground",
            )}
          />
        }
      >
        <span className="flex min-w-0 items-center gap-2">
          <Search className="size-4 shrink-0 opacity-50" />
          <span className="truncate">{selectedProduct?.name ?? "Buscar producto..."}</span>
        </span>
        <ChevronsUpDown className="size-4 shrink-0 opacity-50" />
      </PopoverTrigger>

      <PopoverContent className="w-[var(--radix-popover-trigger-width)] min-w-[260px] p-0" align="start">
        <Command>
          <CommandInput placeholder="Buscar producto..." />

          <CommandList>
            {isLoading && (
              <CommandEmpty>
                Cargando productos...
              </CommandEmpty>
            )}

            {isError && (
              <CommandEmpty>
                No se pudieron cargar los productos.
              </CommandEmpty>
            )}

            {!isLoading && !isError && (
              <>
                <CommandEmpty>
                  <div className="flex flex-col items-center gap-3 py-2">
                    <p className="text-sm text-muted-foreground">
                      No se encontraron productos
                    </p>

                    {onRequestCreate && (
                      <Button
                        size="sm"
                        variant="secondary"
                        onClick={() => {
                          setOpen(false);
                          onRequestCreate();
                        }}
                      >
                        <Plus className="size-4" />
                        Registrar producto
                      </Button>
                    )}
                    
                  </div>
                </CommandEmpty>

                <CommandGroup>
                  {products.map((product) => (
                    <CommandItem
                      key={product.id}
                      value={product.name}
                      onSelect={() => {
                        onSelect(product);
                        setOpen(false);
                      }}
                      className="flex items-start gap-2"
                    >
                      <Check
                        className={cn(
                          "mt-0.5 size-4",
                          selectedId === product.id
                            ? "opacity-100"
                            : "opacity-0",
                        )}
                      />

                      <span className="flex flex-col">
                        <span className="text-sm font-medium">
                          {product.name}
                        </span>

                        <span className="text-xs text-muted-foreground">
                          Lote: {product.batchNumber ?? "Sin lote"}
                          {" · "}
                          Stock: {product.stock}
                        </span>
                      </span>
                    </CommandItem>
                  ))}
                </CommandGroup>
              </>
            )}

          </CommandList>
        </Command>
      </PopoverContent>
    </Popover>
  );
}
