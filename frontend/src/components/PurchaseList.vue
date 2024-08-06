<template>
    <div>
      <h2>Existing Stocks</h2>
    <div class="stock-grid">
      <div v-for="stock in stocks" :key="stock.id" class="stock-card">
        <h3>{{ stock.stockName }}</h3>
        <p><strong>Price:</strong> ${{ stock.price }}</p>
        <p><strong>Quantity:</strong> {{ stock.amount }}</p>        
        <p><strong>Total Price:</strong> ${{ stock.totalPrice }}</p>
        <p><strong>Purchase Date:</strong> {{ new Date(stock.purchaseDate).toLocaleDateString() }}</p>
      </div>
    </div>

    <div class="form-container">
      <h3>Add New Stock</h3>
    <form @submit.prevent="addStock" class="add-stock-form">
      <div class="form-group">
        <label for="name">Stock Name:</label>
        <select v-model="newStock.stockId" v-on:change="validatePurchase" required>
          <option v-for="stock in availableStocks" :key="stock.id" :value="stock.id">
            {{ stock.name }}
          </option>
        </select>
      </div>
      <div class="form-group">
          <label for="price">Price: ${{newStock.price}}</label>        
        </div>
      <div class="form-group">
        <label for="amount">Amount:</label>
        <input type="number" v-model="newStock.amount" v-on:change="validatePurchase" required />  
      </div>      
      <div class="form-group" v-if="newStock.error==''">
          <label for="totalPrice">Total Price: {{newStock.price*newStock.amount}}</label>        
        </div>
        <div class="form-group error" v-if="newStock.error!=''">
          {{newStock.error}}      
        </div>
      <button :disabled="newStock.error!=''||newStock.amount<=0" type="submit">Add Stock</button>
    </form>
  </div>
  <!-- Toast Notification -->
  <div v-if="showToast" class="toast">
      Stock purchased successfully!
    </div>
  </div>
  </template>
  
  <script>
  export default {

    data() {
      return {
        stocks: [],
        availableStocks:[],
        newStock: {
          clientId:'89e8d857-3e13-4d47-b53b-04b0eda248ea',
          stockId: '',
          amount: 0,
          price: 0,
          error:''
      },
      showToast: false
      };
    },
    created() {      
      this.fetchAvailableStocks();
      this.fetchStocks();
    },
    methods: {
      async fetchStocks() {
        try {
          const response = await fetch(`${process.env.VUE_APP_API_BASE_URL}/Purchases/89e8d857-3e13-4d47-b53b-04b0eda248ea`);
          this.stocks = await response.json();
        } catch (error) {
          console.error('Error fetching stocks:', error);
        }
      }, 
      async fetchAvailableStocks() {
        try {
          const response = await fetch(`${process.env.VUE_APP_API_BASE_URL}/Stocks`);
          this.availableStocks = await response.json();
        } catch (error) {
          console.error('Error fetching available stocks:', error);
        }
      },     
      async addStock() {
        try {
          const response = await fetch(`${process.env.VUE_APP_API_BASE_URL}/Purchases`, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify(this.newStock)
          });

          if (response.ok) {
            this.newStock = { stockId: '', amount: 0, price: 0, error:'',clientId:'89e8d857-3e13-4d47-b53b-04b0eda248ea' };
            this.fetchStocks(); // Refresh the stock list
            this.$emit('stock-added'); // Emit the event
            this.showToastNotification();
          } else {
            var error=await response.json();
            console.error('Error adding stock:', error);
            this.newStock.error =  error;
          }
        } catch (error) {
          console.error('Error adding stock:', error);
          this.newStock.error =  error;
        }
      },    
      async validatePurchase() {

        if (!this.newStock.stockId) {
          this.newStock.price = 0;
          return;
        }

        const selectedStock = this.availableStocks.find(
          (stock) => stock.id === this.newStock.stockId
        );
        this.newStock.price = selectedStock.price;
        
        if (!this.newStock.stockId || this.newStock.amount <= 0) {
          this.newStock.error = '';
          return;
        }        

        try {
          const response = await fetch(`${process.env.VUE_APP_API_BASE_URL}/purchases/validate`, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify(this.newStock)
          });

          if (response.ok) {
            this.newStock.error = '';
          } else {
            var error=await response.json();
            console.error('Error validating purchase:', error);
            this.newStock.error =  error;
          }
        } catch (error) {
          console.error('Error validating purchase:', error);
          this.newStock.error =  error;
        }
      },
      showToastNotification() {
        this.showToast = true;
        setTimeout(() => {
          this.showToast = false;
        }, 5000); // Toast disappears after 5 seconds
      },
    },
  };
  </script>
  
  <style scoped>
   
/* Grid layout for the stock items */
.stock-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  gap: 20px;
  margin-top: 20px;
}

/* Individual stock card styling */
.stock-card {
  border: 1px solid #ccc;
  border-radius: 8px;
  padding: 16px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  background-color: #fff;
}

.stock-card h3 {
  margin: 0 0 10px;
  font-size: 18px;
  color: #333;
}

.stock-card p {
  margin: 5px 0;
  color: #555;
}

/* Container for the add stock form */
.form-container {
  margin-top: 40px;
  padding: 20px;
  max-width: 500px;
  background-color: #f9f9f9;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

/* Styling for the form */
.add-stock-form {
  display: flex;
  flex-direction: column;
}

/* Form group styling */
.form-group {
  margin-bottom: 20px;
}

.form-group label {
  display: block;
  margin-bottom: 5px;
  font-weight: bold;
  color: #333;
}

.form-group input,
.form-group select {
  width: 100%;
  padding: 10px;
  box-sizing: border-box;
  border: 1px solid #ccc;
  border-radius: 4px;
  font-size: 16px;
}

.form-group input:focus,
.form-group select:focus {
  border-color: #007bff;
  outline: none;
  box-shadow: 0 0 5px rgba(0, 123, 255, 0.25);
}

.error {
  color: red;
  font-weight: bold;
}

/* Button styling */
button {
  padding: 12px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 4px;
  font-size: 16px;
  cursor: pointer;
  transition: background-color 0.3s;
}

button:hover {
  background-color: #0056b3;
}

button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

button:focus {
  outline: none;
  box-shadow: 0 0 5px rgba(0, 123, 255, 0.5);
}

/* Styling for the toast notification */
.toast {
  position: fixed;
  bottom: 20px;
  right: 20px;
  background-color: #28a745;
  color: white;
  padding: 15px;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.2);
  font-size: 16px;
  z-index: 1000;
  transition: opacity 0.5s ease-in-out;
}

/* Fade out effect */
.toast-fade-out {
  opacity: 0;
}
  </style>
  