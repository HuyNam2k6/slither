# Quy Tắc Bản Quyền Asset & Phát Hành Thương Mại

File này quy định tiêu chuẩn chọn và sử dụng tài nguyên (Model 3D, Sprite 2D, Âm thanh, Font, Code) cho dự án game, đảm bảo **hợp pháp 100% khi phát hành công khai và kiếm tiền** (Quảng cáo, IAP, bán game trên Web / Google Play / App Store / Steam).

---

## 1. Các Giấy Phép ĐƯỢC PHÉP Dùng Kiếm Tiền

### Ưu tiên hàng đầu (Không cần ghi nguồn)
* **CC0 (Creative Commons Zero / Public Domain):** Miễn phí hoàn toàn, được phép chỉnh sửa, phân phối và kiếm tiền không giới hạn. Không bắt buộc ghi tên tác giả.
* **MIT / BSD / Apache 2.0:** Dùng cho thư viện code, shader, hoặc plugin. Được phép dùng thương mại.

### Chấp nhận có điều kiện (Phải ghi nguồn)
* **CC-BY (Creative Commons Attribution):** Được phép dùng thương mại và chỉnh sửa, **NHƯNG bắt buộc phải ghi tên tác giả** (trong màn hình `Credits` của game hoặc file `CREDITS.md`).
* **Royalty-Free (Commercial Use Allowed):** Được phép dùng thương mại theo điều khoản của cửa hàng (ví dụ: Unity Asset Store, Fab/Unreal Marketplace). Không được bán lại nguyên gốc file asset.

---

## 2. Các Giấy Phép & Nguồn CẤM TUYỆT ĐỐI

* **CC-BY-NC / Bất kỳ giấy phép nào có chữ `NC` (Non-Commercial):** Cấm dùng để kiếm tiền dưới mọi hình thức (kể cả gắn quảng cáo).
* **Personal Use Only / Educational Use Only:** Chỉ dùng để học tập, không được phát hành.
* **CC-BY-ND (No Derivatives):** Không cho phép chỉnh sửa/cắt ghép asset.
* **Asset rip từ game khác** (ví dụ: lấy model/nhạc của *Slither.io*, *Pokemon*, *Minecraft*...): Vi phạm bản quyền nặng, chắc chắn bị gỡ game hoặc khóa tài khoản store.
* **Font chữ mặc định không rõ bản quyền:** Chỉ dùng font có giấy phép **SIL Open Font License (OFL)** (ví dụ: Google Fonts) hoặc **CC0**.

---

## 3. Quy Trình Kiểm Tra Trước Khi Đưa Asset Vào Project

Mỗi khi tải bất kỳ model, hình ảnh, hay âm thanh nào về, **bắt buộc** kiểm tra đủ 3 bước:
1. **Xác minh License:** Đọc kỹ file `LICENSE.txt` hoặc mục License trên trang tải (Kenney, Quaternius, Itch.io, OpenGameArt, Freesound...).
2. **Lưu trữ thông tin:** Ghi lại vào bảng theo dõi (hoặc file `CREDITS.md`) gồm:
   * Tên asset
   * Link nguồn tải
   * Loại giấy phép (`CC0`, `CC-BY 4.0`, `OFL`, v.v.)
   * Nội dung cần ghi nguồn (nếu là `CC-BY`)
3. **Đặt tên & phân loại:** Chỉ đưa vào thư mục `assets/` của dự án những file đã qua kiểm duyệt.

---

## 4. Nguồn Asset Sạch Gợi Ý (Chuẩn CC0)

* **3D Models:** `quaternius.com`, `kenney.nl/assets`
* **2D Sprites & UI:** `kenney.nl/assets`, `itch.io` (lọc tag `Free` + `CC0`)
* **Âm thanh / Nhạc nền:** `freesound.org` (lọc **CC0**), `kenney.nl/assets` (Audio packs)
* **Font chữ:** `fonts.google.com` (chuẩn **OFL** — miễn phí thương mại)
