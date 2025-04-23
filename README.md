# 🔍 Simple 2D Pathfinding in Unity

Đây là một project Unity đơn giản mô phỏng cơ chế tìm đường trong mê cung 2D dựa trên **Grid**. NPC sẽ tìm đường đi đến đích thông qua thuật toán duyệt theo **4 hướng (trên, dưới, trái, phải)**, tránh các ô là tường.

---

## 🚀 Cách sử dụng

### 1. Clone và mở bằng Unity
Clone repo này và mở bằng Unity (khuyên dùng Unity 2021 trở lên).

### 2. Cấu hình và tạo bản đồ
- Trong **Hierarchy**, chọn GameObject `BoardManager`.
  - Tại đây bạn sẽ thấy biến `Wall Spawn Chance` để điều chỉnh tỉ lệ random tường khi tạo map.
- Ở góc **trên bên trái màn hình**, nhập kích thước X và Y rồi nhấn **Generate Map** để tạo bản đồ.

> ⚠️ **Lưu ý**: Ngoại trừ `Wall Spawn Chance`, các lựa chọn như đặt NPC, Goal, hoặc tìm/xoá đường **chỉ có thể sử dụng sau khi bản đồ đã được tạo**.

### 3. Đặt NPC và Goal
- Ở góc phải **bên dưới**, chọn Toggle tương ứng để đặt **NPC** hoặc **Goal**.
- Sau đó nhấn vào ô bất kỳ trên bản đồ để đặt vị trí tương ứng.

### 4. Tìm đường hoặc xoá đường
- Ở góc dưới **bên trái**, chọn:
  - 🟡 **Find Path**: tìm đường đi từ NPC đến Goal, vẽ đường màu vàng và log ra danh sách tọa độ.
  - ❌ **Clear Path**: xoá NPC, Goal và đường đi (tường sẽ giữ nguyên).

---

## 📌 Ghi chú kỹ thuật

- NPC chỉ tìm đường đi theo **4 hướng**: trên, dưới, trái, phải.
- Tường chỉ được sinh ngẫu nhiên khi bấm **Generate Map** và **không thể chỉnh tay**.
- Các ô được hiển thị bằng `SpriteRenderer`, và tự động scale để nằm trọn trong vùng `safe area` (giữa hai điểm đánh dấu trong màn hình).

---

🛠️ Project có thể dành cho học tập, thực hành và làm nền tảng cho các game mô phỏng logic hoặc chiến thuật!
