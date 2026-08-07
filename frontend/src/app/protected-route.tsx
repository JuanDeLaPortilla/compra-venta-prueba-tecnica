import { Navigate, Outlet } from "react-router";
import { tokenStorage } from "@/core/storage/token-storage";

export const ProtectedRoute = () => {
  const token = tokenStorage.get();

  if (!token) {
    return (
      <Navigate
        to="/login"
        replace
      />
    );
  }

  return <Outlet />;
};