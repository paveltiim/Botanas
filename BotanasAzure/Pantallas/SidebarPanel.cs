using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Aires.Pantallas
{
    /// <summary>
    /// Panel lateral estilo ERP (minimalista, colapsable) para la navegación principal.
    /// Agrupa los módulos del sistema en secciones con submenús colapsables.
    /// Compatible con control de permisos por rol de usuario.
    /// </summary>
    public class SidebarPanel : Panel
    {
        #region Constantes y Colores

        private const int EXPANDED_WIDTH = 230;
        private const int COLLAPSED_WIDTH = 52;
        private const int HEADER_HEIGHT = 56;
        private const int SEARCH_HEIGHT = 42;
        private const int ITEM_HEIGHT = 36;
        private const int GROUP_HEADER_HEIGHT = 38;

        private static readonly Color COLOR_BG             = Color.FromArgb(28, 40, 54);
        private static readonly Color COLOR_HOVER          = Color.FromArgb(45, 61, 80);
        private static readonly Color COLOR_ACTIVE         = Color.FromArgb(0, 120, 212);
        private static readonly Color COLOR_GROUP_HEADER   = Color.FromArgb(20, 30, 42);
        private static readonly Color COLOR_TEXT           = Color.FromArgb(220, 235, 250);
        private static readonly Color COLOR_SUBTEXT        = Color.FromArgb(140, 165, 195);
        private static readonly Color COLOR_SEARCH_BG      = Color.FromArgb(40, 55, 72);
        private static readonly Color COLOR_TOGGLE_BG      = Color.FromArgb(0, 100, 180);
        private static readonly Color COLOR_SEPARATOR      = Color.FromArgb(45, 61, 80);

        #endregion

        #region Clases internas

        private class SidebarItem
        {
            public string Key        { get; set; }
            public string Text       { get; set; }
            public string GroupKey   { get; set; }
            public string Icon       { get; set; }
            public string Tooltip    { get; set; }
            public Button Button     { get; set; }
            public EventHandler OnClick { get; set; }
            public bool PermVisible  { get; set; } = false;
        }

        private class SidebarGroup
        {
            public string Key           { get; set; }
            public string Title         { get; set; }
            public string Icon          { get; set; }
            public Button HeaderButton  { get; set; }
            public Panel  ContentPanel  { get; set; }
            public bool   IsExpanded    { get; set; } = true;
            public List<string> ItemKeys { get; set; } = new List<string>();
        }

        #endregion

        #region Campos privados

        private bool _isExpanded = true;

        private Panel       _headerPanel;
        private Button      _toggleBtn;
        private Label       _titleLabel;
        private Panel       _searchPanel;
        private TextBox     _searchBox;
        private bool        _searchHasPlaceholder = true;
        private Panel       _contentArea;
        private ToolTip     _toolTip;

        private readonly Dictionary<string, SidebarItem>  _items  = new Dictionary<string, SidebarItem>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, SidebarGroup> _groups = new Dictionary<string, SidebarGroup>(StringComparer.OrdinalIgnoreCase);
        private readonly List<string> _groupOrder = new List<string>();

        private SidebarItem _activeItem;

        #endregion

        #region Constructor e inicialización

        public SidebarPanel()
        {
            _toolTip = new ToolTip { InitialDelay = 500, ShowAlways = true };
            InitializeSidebar();
        }

        private void InitializeSidebar()
        {
            this.Width    = EXPANDED_WIDTH;
            this.Dock     = DockStyle.Left;
            this.BackColor = COLOR_BG;
            this.Padding  = new Padding(0);

            // --- Encabezado (logo + toggle) ---
            _headerPanel = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = HEADER_HEIGHT,
                BackColor = COLOR_GROUP_HEADER,
                Padding   = new Padding(0)
            };

            _toggleBtn = new Button
            {
                Text      = "☰",
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = COLOR_TOGGLE_BG,
                Font      = new Font("Segoe UI", 14f, FontStyle.Bold),
                Size      = new Size(COLLAPSED_WIDTH, HEADER_HEIGHT),
                Location  = new Point(0, 0),
                Cursor    = Cursors.Hand,
                TabStop   = false
            };
            _toggleBtn.FlatAppearance.BorderSize = 0;
            _toggleBtn.Click += ToggleBtn_Click;
            _toolTip.SetToolTip(_toggleBtn, "Expandir / Contraer menú");

            _titleLabel = new Label
            {
                Text      = "BOTANAS JAVY",
                ForeColor = COLOR_TEXT,
                BackColor = Color.Transparent,
                Font      = new Font("Segoe UI", 10f, FontStyle.Bold),
                AutoSize  = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Location  = new Point(COLLAPSED_WIDTH + 6, 0),
                Size      = new Size(EXPANDED_WIDTH - COLLAPSED_WIDTH - 10, HEADER_HEIGHT)
            };

            _headerPanel.Controls.Add(_titleLabel);
            _headerPanel.Controls.Add(_toggleBtn);
            _toggleBtn.BringToFront();

            // --- Buscador rápido de módulos ---
            _searchPanel = new Panel
            {
                Dock      = DockStyle.Top,
                Height    = SEARCH_HEIGHT,
                BackColor = COLOR_BG,
                Padding   = new Padding(8, 6, 8, 6)
            };

            _searchBox = new TextBox
            {
                Dock        = DockStyle.Fill,
                BackColor   = COLOR_SEARCH_BG,
                ForeColor   = COLOR_SUBTEXT,
                BorderStyle = BorderStyle.FixedSingle,
                Font        = new Font("Segoe UI", 9f),
                Text        = "🔍 Buscar módulo..."
            };
            _searchHasPlaceholder = true;
            _searchBox.GotFocus   += SearchBox_GotFocus;
            _searchBox.LostFocus  += SearchBox_LostFocus;
            _searchBox.TextChanged += SearchBox_TextChanged;

            _searchPanel.Controls.Add(_searchBox);

            // --- Área de contenido desplazable ---
            _contentArea = new Panel
            {
                Dock       = DockStyle.Fill,
                AutoScroll = true,
                BackColor  = COLOR_BG
            };

            this.Controls.Add(_contentArea);
            this.Controls.Add(_searchPanel);
            this.Controls.Add(_headerPanel);

            // Barra de separación a la derecha del sidebar
            this.Paint += SidebarPanel_Paint;
        }

        private void SidebarPanel_Paint(object sender, PaintEventArgs e)
        {
            // Línea separadora en el borde derecho
            using (var pen = new Pen(COLOR_SEPARATOR, 1))
                e.Graphics.DrawLine(pen, this.Width - 1, 0, this.Width - 1, this.Height);
        }

        #endregion

        #region API pública

        /// <summary>Agrega un grupo (sección colapsable) al sidebar.</summary>
        public void AddGroup(string key, string title, string icon = "▶")
        {
            if (_groups.ContainsKey(key)) return;

            var group = new SidebarGroup
            {
                Key        = key,
                Title      = title,
                Icon       = icon,
                IsExpanded = true
            };

            // Botón de encabezado del grupo
            var headerBtn = new Button
            {
                Text      = "▼  " + icon + "  " + title,
                TextAlign = ContentAlignment.MiddleLeft,
                FlatStyle = FlatStyle.Flat,
                ForeColor = COLOR_SUBTEXT,
                BackColor = COLOR_GROUP_HEADER,
                Font      = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                Width     = EXPANDED_WIDTH,
                Height    = GROUP_HEADER_HEIGHT,
                Padding   = new Padding(10, 0, 0, 0),
                Cursor    = Cursors.Hand,
                TabStop   = false,
                Tag       = key
            };
            headerBtn.FlatAppearance.BorderSize = 0;
            headerBtn.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 43, 57);
            headerBtn.Click += GroupHeader_Click;

            // Panel de contenido del grupo
            var contentPanel = new Panel
            {
                Width     = EXPANDED_WIDTH,
                Height    = 0,
                BackColor = COLOR_BG,
                Padding   = new Padding(0),
                Visible   = true
            };

            group.HeaderButton = headerBtn;
            group.ContentPanel = contentPanel;
            _groups[key] = group;
            _groupOrder.Add(key);

            // Agregar al área de contenido (layout manual por Y)
            int currentY = GetNextY();
            headerBtn.Location  = new Point(0, currentY);
            contentPanel.Location = new Point(0, currentY + GROUP_HEADER_HEIGHT);

            _contentArea.Controls.Add(headerBtn);
            _contentArea.Controls.Add(contentPanel);

            UpdateContentAreaHeight();
        }

        /// <summary>Agrega un ítem de menú dentro de un grupo existente.</summary>
        public void AddItem(string key, string text, string groupKey, EventHandler onClick, string icon = "–", string tooltip = null)
        {
            if (!_groups.ContainsKey(groupKey) || _items.ContainsKey(key)) return;

            var group = _groups[groupKey];

            var btn = new Button
            {
                Text      = "      " + icon + "  " + text,
                TextAlign = ContentAlignment.MiddleLeft,
                FlatStyle = FlatStyle.Flat,
                ForeColor = COLOR_TEXT,
                BackColor = COLOR_BG,
                Font      = new Font("Segoe UI", 9f),
                Width     = EXPANDED_WIDTH,
                Height    = ITEM_HEIGHT,
                Padding   = new Padding(0),
                Cursor    = Cursors.Hand,
                TabStop   = false,
                Visible   = false
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = COLOR_HOVER;

            var item = new SidebarItem
            {
                Key        = key,
                Text       = text,
                GroupKey   = groupKey,
                Icon       = icon,
                Tooltip    = tooltip ?? text,
                Button     = btn,
                OnClick    = onClick,
                PermVisible = false
            };

            btn.Tag = item;
            btn.Click      += SidebarItem_Click;
            btn.MouseEnter += SidebarItem_MouseEnter;
            btn.MouseLeave += SidebarItem_MouseLeave;

            _toolTip.SetToolTip(btn, item.Tooltip);

            group.ItemKeys.Add(key);
            _items[key] = item;

            // Insertar en contentPanel
            int yOffset = (group.ItemKeys.Count - 1) * ITEM_HEIGHT;
            btn.Location = new Point(0, yOffset);
            group.ContentPanel.Controls.Add(btn);
            group.ContentPanel.Height = group.ItemKeys.Count * ITEM_HEIGHT;

            UpdateGroupPositions();
            UpdateContentAreaHeight();
        }

        /// <summary>Oculta todos los ítems (llamar antes de aplicar permisos de rol).</summary>
        public void SetAllItemsHidden()
        {
            foreach (var item in _items.Values)
            {
                item.PermVisible = false;
                item.Button.Visible = false;
            }
            foreach (var group in _groups.Values)
                RefreshGroupContentHeight(group.Key);
        }

        /// <summary>Muestra u oculta un ítem del menú por su clave.</summary>
        public void SetItemVisible(string key, bool visible)
        {
            if (!_items.ContainsKey(key)) return;
            var item = _items[key];
            item.PermVisible = visible;
            item.Button.Visible = visible;
            RefreshGroupContentHeight(item.GroupKey);
        }

        /// <summary>Habilita o deshabilita un ítem del menú por su clave.</summary>
        public void SetItemEnabled(string key, bool enabled)
        {
            if (!_items.ContainsKey(key)) return;
            _items[key].Button.Enabled = enabled;
        }

        #endregion

        #region Layout interno

        private int GetNextY()
        {
            int y = 0;
            foreach (var gKey in _groupOrder)
            {
                if (!_groups.ContainsKey(gKey)) continue;
                var g = _groups[gKey];
                y += GROUP_HEADER_HEIGHT + g.ContentPanel.Height;
            }
            return y;
        }

        private void UpdateGroupPositions()
        {
            int y = 0;
            foreach (var gKey in _groupOrder)
            {
                if (!_groups.ContainsKey(gKey)) continue;
                var g = _groups[gKey];
                g.HeaderButton.Location  = new Point(0, y);
                y += GROUP_HEADER_HEIGHT;
                g.ContentPanel.Location = new Point(0, y);
                y += g.ContentPanel.Height;
            }
        }

        private void RefreshGroupContentHeight(string groupKey)
        {
            if (!_groups.ContainsKey(groupKey)) return;
            var group = _groups[groupKey];

            if (!group.IsExpanded)
            {
                group.ContentPanel.Height = 0;
            }
            else
            {
                int y = 0;
                foreach (var key in group.ItemKeys)
                {
                    if (!_items.ContainsKey(key)) continue;
                    var item = _items[key];
                    if (item.Button.Visible)
                    {
                        item.Button.Location = new Point(0, y);
                        y += ITEM_HEIGHT;
                    }
                }
                group.ContentPanel.Height = y;
            }

            UpdateGroupPositions();
            UpdateContentAreaHeight();
        }

        private void UpdateContentAreaHeight()
        {
            int total = 0;
            foreach (var gKey in _groupOrder)
            {
                if (!_groups.ContainsKey(gKey)) continue;
                var g = _groups[gKey];
                total += GROUP_HEADER_HEIGHT + g.ContentPanel.Height;
            }
            // El AutoScroll del Panel gestiona el scroll si total > Height
        }

        #endregion

        #region Eventos: Toggle (colapsar/expandir)

        private void ToggleBtn_Click(object sender, EventArgs e)
        {
            _isExpanded = !_isExpanded;
            int targetWidth = _isExpanded ? EXPANDED_WIDTH : COLLAPSED_WIDTH;

            this.Width = targetWidth;
            _titleLabel.Visible  = _isExpanded;
            _searchPanel.Visible = _isExpanded;

            foreach (var gKey in _groupOrder)
            {
                if (!_groups.ContainsKey(gKey)) continue;
                var group = _groups[gKey];

                group.HeaderButton.Width = targetWidth;

                if (_isExpanded)
                {
                    string arrow = group.IsExpanded ? "▼" : "▶";
                    group.HeaderButton.Text = arrow + "  " + group.Icon + "  " + group.Title;
                    group.HeaderButton.TextAlign = ContentAlignment.MiddleLeft;
                    group.ContentPanel.Visible = group.IsExpanded;
                }
                else
                {
                    group.HeaderButton.Text = group.Icon;
                    group.HeaderButton.TextAlign = ContentAlignment.MiddleCenter;
                    group.ContentPanel.Visible = false;
                }

                group.ContentPanel.Width = targetWidth;

                foreach (var key in group.ItemKeys)
                {
                    if (!_items.ContainsKey(key)) continue;
                    var item = _items[key];
                    item.Button.Width = targetWidth;
                    if (_isExpanded)
                    {
                        item.Button.Text = "      " + item.Icon + "  " + item.Text;
                        item.Button.TextAlign = ContentAlignment.MiddleLeft;
                    }
                    else
                    {
                        item.Button.Text = item.Icon;
                        item.Button.TextAlign = ContentAlignment.MiddleCenter;
                    }
                }
            }

            UpdateGroupPositions();
        }

        #endregion

        #region Eventos: Grupo (colapsar/expandir sección)

        private void GroupHeader_Click(object sender, EventArgs e)
        {
            if (!_isExpanded) return;

            var btn = (Button)sender;
            var key = (string)btn.Tag;
            if (!_groups.ContainsKey(key)) return;

            var group = _groups[key];
            group.IsExpanded = !group.IsExpanded;
            group.ContentPanel.Visible = group.IsExpanded;

            string arrow = group.IsExpanded ? "▼" : "▶";
            group.HeaderButton.Text = arrow + "  " + group.Icon + "  " + group.Title;

            RefreshGroupContentHeight(key);
        }

        #endregion

        #region Eventos: Ítem de menú

        private void SidebarItem_Click(object sender, EventArgs e)
        {
            var btn  = (Button)sender;
            var item = (SidebarItem)btn.Tag;

            // Desactivar ítem previo
            if (_activeItem != null)
            {
                _activeItem.Button.BackColor = COLOR_BG;
                _activeItem.Button.ForeColor = COLOR_TEXT;
            }

            // Activar ítem actual
            _activeItem = item;
            item.Button.BackColor = COLOR_ACTIVE;
            item.Button.ForeColor = Color.White;

            item.OnClick?.Invoke(sender, e);
        }

        private void SidebarItem_MouseEnter(object sender, EventArgs e)
        {
            var btn  = (Button)sender;
            var item = (SidebarItem)btn.Tag;
            if (item != _activeItem)
                btn.BackColor = COLOR_HOVER;
        }

        private void SidebarItem_MouseLeave(object sender, EventArgs e)
        {
            var btn  = (Button)sender;
            var item = (SidebarItem)btn.Tag;
            if (item != _activeItem)
                btn.BackColor = COLOR_BG;
        }

        #endregion

        #region Eventos: Buscador rápido

        private void SearchBox_GotFocus(object sender, EventArgs e)
        {
            if (_searchHasPlaceholder)
            {
                _searchHasPlaceholder = false;
                _searchBox.Text = string.Empty;
                _searchBox.ForeColor = COLOR_TEXT;
            }
        }

        private void SearchBox_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_searchBox.Text))
            {
                _searchHasPlaceholder = true;
                _searchBox.Text = "🔍 Buscar módulo...";
                _searchBox.ForeColor = COLOR_SUBTEXT;
            }
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            if (_searchHasPlaceholder) return;

            string term = _searchBox.Text.Trim().ToLowerInvariant();

            if (string.IsNullOrEmpty(term))
            {
                // Restaurar visibilidad según permisos
                foreach (var item in _items.Values)
                    item.Button.Visible = item.PermVisible;
                foreach (var gKey in _groupOrder)
                    RefreshGroupContentHeight(gKey);
                return;
            }

            // Filtrar ítems
            foreach (var item in _items.Values)
            {
                bool match = item.PermVisible &&
                             (item.Text.ToLowerInvariant().Contains(term) ||
                              item.Tooltip.ToLowerInvariant().Contains(term));
                item.Button.Visible = match;
            }

            foreach (var gKey in _groupOrder)
            {
                if (!_groups.ContainsKey(gKey)) continue;
                var group = _groups[gKey];
                bool anyVisible = false;
                foreach (var k in group.ItemKeys)
                {
                    if (_items.ContainsKey(k) && _items[k].Button.Visible)
                    {
                        anyVisible = true;
                        break;
                    }
                }
                group.HeaderButton.Visible = anyVisible;
                group.ContentPanel.Visible = anyVisible;
                RefreshGroupContentHeight(gKey);
            }
        }

        #endregion
    }
}
