import { BASE_TITLE } from "@/shared/constants";
import { Helmet } from "react-helmet-async";
import { LoginForm } from "./components/login-form";

const LoginPage = () => {
  return (
    <>
      <Helmet>
        <title>Login | {BASE_TITLE}</title>
      </Helmet>

      <main className="flex min-h-screen flex-col justify-center px-6 py-12 lg:px-8">
        
        <header className="sm:mx-auto sm:w-full sm:max-w-sm">
          <h1>Iniciar Sesión</h1>

          <p className="text-muted-foreground">
            Ingresa tu usuario para iniciar sesión
          </p>
        </header>

        <section className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
          <LoginForm />
        </section>

      </main>
    </>
  );
};

export default LoginPage;