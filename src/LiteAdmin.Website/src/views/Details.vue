<template>
    <div class="home">
        <h1>Details</h1>
        <div v-for="(column, index) in tableSchema.columns">
            <label>{{column.name}}</label>
            <div>{{item[column.name]}}</div>            
        </div>                        
    </div>
</template>

<script lang="ts" src="./Details.ts"></script>
