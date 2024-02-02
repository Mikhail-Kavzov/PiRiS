export interface User
{
    userId?: string;
    email?: string | undefined;
    permissions?: string[] | undefined;
    roles?: string[] | undefined;
    fullName?: string | undefined;
}
