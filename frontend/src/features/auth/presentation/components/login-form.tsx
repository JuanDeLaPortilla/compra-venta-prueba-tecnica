import { useForm } from "react-hook-form";
import { useNavigate } from "react-router";

import { Button } from "@/shared/components/ui/button";
import {
  Field,
  FieldLabel,
} from "@/shared/components/ui/field";
import { Input } from "@/shared/components/ui/input";

import type { LoginRequest } from "../../domain/auth";
import { useLogin } from "../../hooks/use-login";

export const LoginForm = () => {
  const navigate = useNavigate();

  const loginMutation = useLogin();

  const {
    register,
    handleSubmit,
    formState: {
      errors,
    },
  } = useForm<LoginRequest>({
    defaultValues: {
      username: "",
      password: "",
    },
  });

  const onSubmit = (data: LoginRequest) => {
    loginMutation.mutate(data, {
      onSuccess: () => {
        navigate("/");
      },
    });
  };

  return (
    <form
      onSubmit={handleSubmit(onSubmit)}
      className="space-y-5"
    >
      <Field>
        <FieldLabel htmlFor="username">
          Usuario
        </FieldLabel>

        <Input
          id="username"
          placeholder="john_doe"
          {...register("username", {
            required: "El usuario es obligatorio",
          })}
        />

        {errors.username && (
          <p className="text-sm text-destructive">
            {errors.username.message}
          </p>
        )}
      </Field>

      <Field>
        <FieldLabel htmlFor="password">
          Contraseña
        </FieldLabel>

        <Input
          id="password"
          type="password"
          placeholder="••••••••"
          {...register("password", {
            required: "La contraseña es obligatoria",
          })}
        />

        {errors.password && (
          <p className="text-sm text-destructive">
            {errors.password.message}
          </p>
        )}
      </Field>

      {loginMutation.isError && (
        <div className="rounded-md border border-destructive/30 bg-destructive/10 p-3 text-sm text-destructive">
          No se pudo iniciar sesión. Verifica tus
          credenciales.
        </div>
      )}

      <div className="mt-10 w-full">
        <Button
          type="submit"
          className="flex w-full"
          disabled={loginMutation.isPending}
        >
          {loginMutation.isPending
            ? "Iniciando sesión..."
            : "Iniciar Sesión"}
        </Button>
      </div>
    </form>
  );
};