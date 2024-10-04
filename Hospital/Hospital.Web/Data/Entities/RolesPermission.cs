namespace Hospital.Web.Data.Entities
{
    public class RolesPermission
    {
        public Rol Rol { get; set; }
        public int? rolId { get; set; }

        public Permissions Permisos { get; set; }
        public int? PermisosId { get; set; }
    }
}
