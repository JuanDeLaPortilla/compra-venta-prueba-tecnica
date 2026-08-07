import {
  Sidebar,
  SidebarContent, SidebarFooter,
  SidebarGroup, SidebarGroupContent, SidebarHeader,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem, useSidebar
} from "@/shared/components/ui/sidebar.tsx";
import { NavLink } from "react-router";
import { ChevronLeft, ChevronRight, ChevronsUpDown, LogOut, User } from "lucide-react";
import { Button } from "@/shared/components/ui/button";
import { Avatar, AvatarFallback } from "@/shared/components/ui/avatar.tsx";
import { menuItems } from "@/navigation/types";
import { DropdownMenu, DropdownMenuContent, DropdownMenuGroup, DropdownMenuItem, DropdownMenuTrigger } from "@/shared/components/ui/dropdown-menu";
import { useNavigate } from "react-router";
import { tokenStorage } from "@/core/storage/token-storage";
import { useAuth } from "@/features/auth/hooks/use-auth";

export const AppSidebar = () => {

  const { toggleSidebar, open, isMobile } = useSidebar();

  const navigate = useNavigate();

  const handleLogout = () => {
    tokenStorage.remove();

    navigate("/login", {
      replace: true,
    });
  };

  const { username } = useAuth();

  return (
    <Sidebar collapsible="icon">

      <SidebarHeader>
        <SidebarMenu>
          <SidebarMenuItem>
            <SidebarMenuButton
              size="lg"
              className="data-[state=open]:bg-sidebar-accent data-[state=open]:text-sidebar-accent-foreground"
            >
              <div className="flex aspect-square size-8 items-center justify-center rounded-lg">
              </div>
              <div className="grid flex-1 text-left text-sm leading-tight">
                <h3 className="text-primary font-bold">
                  Compra Venta
                </h3>
              </div>
            </SidebarMenuButton>
          </SidebarMenuItem>
        </SidebarMenu>
      </SidebarHeader>

      <SidebarContent>
        {
          !isMobile && (
            <Button
              onClick={toggleSidebar}
              variant="ghost"
              size="icon"
              className="absolute -right-3.5 top-6 z-100 bg-white rounded-full hover:bg-sidebar-accent border border-sidebar-border h-7 w-7"
            >
              {open ? <ChevronLeft /> : <ChevronRight />}
            </Button>
          )
        }
        <SidebarGroup>
          <SidebarGroupContent>
            <SidebarMenu>

              {menuItems.map((item) => {
                const isActive =
                  item.path === "/"
                    ? location.pathname === "/"
                    : location.pathname === item.path ||
                    location.pathname.startsWith(`${item.path}/`);

                return (
                  <SidebarMenuItem
                    key={item.path}
                  >
                    <SidebarMenuButton
                      className="my-1"
                      isActive={isActive}
                      render={
                        <NavLink
                          to={item.path}
                          end={item.path === "/"}
                          className="w-full"
                        />
                      }
                    >
                      <item.icon />
                      {item.title}
                    </SidebarMenuButton>
                  </SidebarMenuItem>
                );
              })}

            </SidebarMenu>
          </SidebarGroupContent>
        </SidebarGroup>
      </SidebarContent>

      <SidebarFooter>
        <SidebarMenu>
          <SidebarMenuItem>
            <DropdownMenu>
              <DropdownMenuTrigger
                render={
                  <SidebarMenuButton
                    size="lg"
                    className="data-[state=open]:bg-sidebar-accent data-[state=open]:text-sidebar-accent-foreground"
                  />
                }>

                <Avatar>
                  <AvatarFallback>
                    <User />
                  </AvatarFallback>
                </Avatar>
                <div className="grid flex-1 text-left text-sm leading-tight">
                  <span className="truncate font-semibold">{username}</span>
                </div>
                <ChevronsUpDown className="ml-auto size-4" />

              </DropdownMenuTrigger>

              <DropdownMenuContent
                className="w-(--radix-dropdown-menu-trigger-width) min-w-56 rounded-lg"
                side={isMobile ? "bottom" : "top"}
                align="end"
                sideOffset={0}
              >
                <DropdownMenuGroup>
                  <DropdownMenuItem onClick={handleLogout}>
                    <LogOut />
                    Cerrar Sesión
                  </DropdownMenuItem>
                </DropdownMenuGroup>
              </DropdownMenuContent>

            </DropdownMenu>

          </SidebarMenuItem>
        </SidebarMenu>

      </SidebarFooter>

    </Sidebar>
  );
};