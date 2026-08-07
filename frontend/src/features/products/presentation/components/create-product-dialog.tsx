import { useEffect } from "react";
import { useForm } from "react-hook-form";
import { Plus } from "lucide-react";

import { Button } from "@/shared/components/ui/button";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from "@/shared/components/ui/dialog";
import { Input } from "@/shared/components/ui/input";
import {
  Field,
  FieldError,
  FieldLabel,
} from "@/shared/components/ui/field";

import type { CreateProductRequest } from "../../domain/product";

interface CreateProductDialogProps {
  open: boolean;
  onOpenChange: (open: boolean) => void;
  onCreate: (request: CreateProductRequest) => void;
  isSubmitting?: boolean;
}

const defaultValues: CreateProductRequest = {
  name: "",
  batchNumber: "",
};

export function CreateProductDialog({
  open,
  onOpenChange,
  onCreate,
}: CreateProductDialogProps) {

  const {
    register,
    handleSubmit,
    reset,
    formState: { errors, isSubmitting },
  } = useForm<CreateProductRequest>({
    defaultValues,
  });

  useEffect(() => {
    if (open) {
      reset(defaultValues);
    }
  }, [open, reset]);

  const onSubmit = (data: CreateProductRequest) => {
    const request: CreateProductRequest = {
      name: data.name.trim(),
      batchNumber: data.batchNumber?.trim() ?? "",
    };

    onCreate(request);
    onOpenChange(false);
  };

  const handleCancel = () => {
    reset(defaultValues);
    onOpenChange(false);
  };

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Registrar producto</DialogTitle>
          <DialogDescription>
            Ingresa los datos básicos del producto.
          </DialogDescription>
        </DialogHeader>

        <form
          id="create-product-form"
          onSubmit={handleSubmit(onSubmit)}
          className="grid gap-4 py-2"
        >
          <Field data-invalid={!!errors.name}>
            <FieldLabel htmlFor="product-name">
              Nombre del producto
            </FieldLabel>

            <Input
              id="product-name"
              placeholder="Producto de Ejemplo"
              autoComplete="off"
              aria-invalid={!!errors.name}
              {...register("name", {
                required: "El nombre del producto es obligatorio.",
                validate: (value) =>
                  value.trim().length > 0 ||
                  "El nombre del producto es obligatorio.",
              })}
            />

            {errors.name && (
              <FieldError errors={[errors.name]} />
            )}
          </Field>

          <Field data-invalid={!!errors.batchNumber}>
            <FieldLabel htmlFor="product-batchNumber">
              Número de Lote
            </FieldLabel>

            <Input
              id="product-batchNumber"
              type="text"
              placeholder="LOT-001"
              aria-invalid={!!errors.batchNumber}
              {...register("batchNumber")}
            />

            {errors.batchNumber && (
              <FieldError errors={[errors.batchNumber]} />
            )}
          </Field>
        </form>

        <DialogFooter>
          <Button
            type="button"
            variant="outline"
            onClick={handleCancel}
            disabled={isSubmitting}
          >
            Cancelar
          </Button>

          <Button
            type="submit"
            form="create-product-form"
            disabled={isSubmitting}
          >
            <Plus className="size-4" />
            {isSubmitting
              ? "Registrando..."
              : "Registrar producto"}
          </Button>
        </DialogFooter>
      </DialogContent>
    </Dialog>
  );
}