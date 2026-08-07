import { BrowserRouter, Route, Routes } from "react-router";
import { lazy, Suspense } from "react";
import { LoadingSpinner } from "@/shared/components/ui/loading-spinner";
import { ProtectedRoute } from "./protected-route";
import { Toaster } from "@/shared/components/ui/sonner";

const LoginPage = lazy(() => import('@/features/auth/presentation/login.page'))

const MainLayout = lazy(() => import('@/shared/layouts/main/main-layout'))

const KardexPage = lazy(() => import('@/features/kardex/presentation/kardex.page'))

const PurchasesPage = lazy(() => import('@/features/purchases/presentation/purchases.page'))
const AddPurchasePage = lazy(() => import('@/features/purchases/presentation/add-purchase.page'))

const SalesPage = lazy(() => import('@/features/sales/presentation/sales.page'))
const AddSalePage = lazy(() => import('@/features/sales/presentation/add-sale.page'))

export function App() {
  return (
    <>
      <Toaster />
      <BrowserRouter>
        <Suspense fallback={<LoadingSpinner />}>

          <Routes>
            <Route path="/login" element={<LoginPage />} />

            <Route element={<ProtectedRoute />}>
              <Route path="/" element={<MainLayout />}>

                <Route path="kardex" element={<KardexPage />}></Route>

                <Route path="purchases" >
                  <Route index element={<PurchasesPage />}></Route>
                  <Route path="add" element={<AddPurchasePage />}></Route>
                </Route>

                <Route path="sales">
                  <Route index element={<SalesPage />}></Route>
                  <Route path="add" element={<AddSalePage />}></Route>
                </Route>

              </Route>
            </Route>

          </Routes>
        </Suspense>
      </BrowserRouter >
    </>
  )
}

export default App
