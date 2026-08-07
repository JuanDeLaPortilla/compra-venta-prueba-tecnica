import { Outlet } from "react-router";
import { SidebarProvider, SidebarTrigger } from "../../components/ui/sidebar";
import { AppSidebar } from "./components/app-sidebar";

const MainLayout = () => {
  return (
    <SidebarProvider>
      <AppSidebar />
      <div className="flex flex-col gap-4 w-full bg-muted/35">
        <div className="md:hidden flex px-6 pt-4">
          <SidebarTrigger />
        </div>

        <div className="max-h-[calc(100vh)] overflow-y-auto w-full lg:flex justify-center">
          <div className="px-6 sm:py-6 py-0 mb-4 lg:w-10/12">
            <main className="flex flex-col gap-4">
              <Outlet />
            </main>
          </div>
        </div>
      </div>
    </SidebarProvider>
  );
}

export default MainLayout;