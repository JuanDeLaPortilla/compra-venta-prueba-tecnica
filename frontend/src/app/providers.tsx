import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { type ReactNode, useState } from "react";
import { HelmetProvider } from "react-helmet-async";

interface AppProvidersProps {
  children: ReactNode;
}

export const AppProviders = ({
  children,
}: AppProvidersProps) => {
  const [queryClient] = useState(
    () =>
      new QueryClient({
        defaultOptions: {
          queries: {
            staleTime: 1000 * 60 * 5,
            retry: 1,
            refetchOnWindowFocus: false,
          },
        },
      })
  );

  return (
    <QueryClientProvider client={queryClient}>
      <HelmetProvider>
        {children}
      </HelmetProvider>
    </QueryClientProvider>
  );
};